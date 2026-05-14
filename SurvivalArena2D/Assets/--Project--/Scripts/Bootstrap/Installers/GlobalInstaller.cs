using System;
using Zenject;

public class GlobalInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        BindPauseManager();
        BindInput();
        BindRecordService();
    }

    private void BindRecordService()
    {
        Container.Bind<RecordsService>().AsSingle().NonLazy();
    }

    private void BindPauseManager()
    {
        Container.BindInterfacesAndSelfTo<PauseManager>().AsSingle().NonLazy();
    }

    private void BindInput()
    {
        Container.BindInterfacesTo<InputReader>().AsSingle().NonLazy();
    }
}
