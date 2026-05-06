using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeSpawner
{
    private GameObject _pickupPrefab;
    private List<UpgradeData> _allUpgrades;
    private List<Transform> _spawnPoints;

    private IInstantiator _instantiator;

    [Inject]
    private void Construct(IInstantiator instantiator, List<UpgradeData> upgrades)
    {
        _instantiator = instantiator;
        _allUpgrades = upgrades;
    }
    public void Init(GameObject pickupPrefab, List<Transform> spawnPoints)
    {
        _pickupPrefab = pickupPrefab;
        _spawnPoints = spawnPoints;
    }
    private void SpawnRandomUpgrades()
    {
        foreach (var point in _spawnPoints)
        {
            UpgradeData randomData = _allUpgrades[Random.Range(0, _allUpgrades.Count)];

            GameObject obj = _instantiator.InstantiatePrefab(_pickupPrefab, point.position, Quaternion.identity, null);

            if (obj.TryGetComponent(out UpgradePickup pickup))
            {
                pickup.Setup(randomData);
            }
        }
    }
}
