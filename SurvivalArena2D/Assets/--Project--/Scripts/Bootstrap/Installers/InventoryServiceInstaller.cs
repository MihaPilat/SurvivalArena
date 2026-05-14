using Zenject;

public class InventoryServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<InventoryService>().AsSingle().NonLazy();
    }
}
