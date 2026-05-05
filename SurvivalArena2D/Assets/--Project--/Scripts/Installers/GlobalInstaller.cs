using Zenject;

public class GlobalInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        BindPauseManager();
        BindInput();
    }

    private void BindPauseManager()
    {
        Container.BindInterfacesAndSelfTo<PauseManager>().AsSingle();
    }

    private void BindInput()
    {
        Container.BindInterfacesTo<InputReader>().AsSingle().NonLazy();
    }
}
