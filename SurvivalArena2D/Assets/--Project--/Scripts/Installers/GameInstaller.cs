using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindProjectileFactory();
    }

    private void BindProjectileFactory()
    {
        Container.Bind<ProjectileFactory>().AsSingle().NonLazy();
    }
}
