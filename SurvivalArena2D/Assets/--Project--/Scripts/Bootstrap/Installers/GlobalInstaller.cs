using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _mainMixer;

    public override void InstallBindings()
    {
        BindPauseManager();
        BindInput();
        BindRecordService();
        BindAudioService();
    }

    private void BindAudioService()
    {
        Container.Bind<AudioService>().AsSingle().WithArguments(_mainMixer);
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
