using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindProjectileFactory();
        BindLevelSystem();
        BindUpgrades();
        BindTimerService();
    }

    private void BindTimerService()
    {
        Container.BindInterfacesAndSelfTo<TimerService>().AsSingle().NonLazy();
    }

    private void BindUpgrades()
    {
        var upgrades = Resources.LoadAll<UpgradeData>("Upgrades");

        Container.Bind<List<UpgradeData>>()
                 .FromInstance(upgrades.ToList())
                 .AsCached()
                 .NonLazy();
    }

    private void BindLevelSystem()
    {
        Container.Bind<ILevelable>().To<LevelSystem>().AsSingle().NonLazy();
    }

    private void BindProjectileFactory()
    {
        Container.Bind<ProjectileFactory>().AsSingle().NonLazy();
    }
}
