using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class UpgradeSpawner: IInitializable, IDisposable
{
    private GameObject _pickupPrefab;
    private List<UpgradeData> _allUpgrades;
    private List<Transform> _spawnPoints;

    private readonly IInstantiator _instantiator;
    private readonly IWaveHandler _waveHandler;

    private GameObject _currentActivePickup;

    [Inject]
    public UpgradeSpawner(IInstantiator instantiator, IWaveHandler waveHandler, List<UpgradeData> upgrades)
    {
        _instantiator = instantiator;
        _waveHandler = waveHandler;
        _allUpgrades = upgrades;
    }

    public void Init(GameObject pickupPrefab, List<Transform> spawnPoints)
    {
        _pickupPrefab = pickupPrefab;
        _spawnPoints = spawnPoints;
    }

    public void Initialize()
    {
        _waveHandler.OnWaveStarted += HandleWaveStarted;
    }

    public void Dispose()
    {
        _waveHandler.OnWaveStarted -= HandleWaveStarted;
    }

    private void HandleWaveStarted(int waveNumber)
    {
        bool isCorrectWave = (waveNumber == 1 || (waveNumber - 1) % 2 == 0);

        bool noActivePickup = _currentActivePickup == null;

        if (isCorrectWave && noActivePickup)
        {
            SpawnRandomUpgrade();
        }
        Debug.Log($"Wave received: {waveNumber}. Correct wave: {isCorrectWave}. No active: {noActivePickup}");
    }

    private void SpawnRandomUpgrade()
    {
        if (_spawnPoints == null || _spawnPoints.Count == 0) return;

        var point = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
        var data = _allUpgrades[Random.Range(0, _allUpgrades.Count)];

        _currentActivePickup = _instantiator.InstantiatePrefab(_pickupPrefab, point.position, Quaternion.identity, null);

        if (_currentActivePickup.TryGetComponent(out UpgradePickup pickup))
        {
            pickup.Setup(data);
        }
    }
}
