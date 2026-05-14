using Zenject;

public class GameStartControllerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<GameStartController>().AsSingle().NonLazy();
    }
}
