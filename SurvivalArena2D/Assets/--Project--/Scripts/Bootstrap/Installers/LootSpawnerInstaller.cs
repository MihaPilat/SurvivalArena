using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LootSpawnerInstaller : MonoInstaller
{
    [SerializeField] private IndicatorManager _indicatorManager;

    [SerializeField] private List<Transform> _defaultPoints;
    [SerializeField] private List<Transform> _specialPoints;

    public override void InstallBindings()
    {
        BindIndicatorManager();
        BindLootConfig();
        BindPointsWithId();
        BindLootSpawner();
    }

    private void BindIndicatorManager()
    {
        Container.BindInstance(_indicatorManager).AsSingle();
    }

    private void BindPointsWithId()
    {
        Container.Bind<List<Transform>>().WithId("DefaultPoints").FromInstance(_defaultPoints).AsCached();

        Container.Bind<List<Transform>>().WithId("SpecialPoints").FromInstance(_specialPoints).AsCached();
    }

    private void BindLootConfig()
    {
        Container.Bind<LootConfig>()
            .FromResource("Configs/LootConfig")
            .AsSingle();
    }

    private void BindLootSpawner()
    {
        Container.BindInterfacesAndSelfTo<LootSpawner>().AsSingle().NonLazy();
    }
}
