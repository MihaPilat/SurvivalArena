using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeSpawnerInstaller : MonoInstaller
{
    [SerializeField] private GameObject _pickupPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    public override void InstallBindings()
    {
        BindUpgradeSpawner();
    }

    private void BindUpgradeSpawner()
    {
        Container.BindInterfacesAndSelfTo<UpgradeSpawner>()
            .AsSingle()
            .OnInstantiated<UpgradeSpawner>((ctx, spawner) =>
            {
                spawner.Init(_pickupPrefab, _spawnPoints);
            })
            .NonLazy();
    }
}
