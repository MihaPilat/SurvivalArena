using System;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindProjectileFactory();
        BindLevelSystem();
    }

    private void BindLevelSystem()
    {
        Container.Bind<ILevelable>().To<LevelSystem>().AsSingle();
    }

    private void BindProjectileFactory()
    {
        Container.Bind<ProjectileFactory>().AsSingle().NonLazy();
    }
}
