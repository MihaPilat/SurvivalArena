using Zenject;

public class MouseInputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMouseInput();
    }

    private void BindMouseInput()
    {
        Container.BindInterfacesTo<MouseInput>().AsSingle().NonLazy();
    }
}
