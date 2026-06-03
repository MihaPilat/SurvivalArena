using System;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private AudioMixer _mainMixer;

    [SerializeField] private CanvasGroup _curtainPrefab;

    public override void InstallBindings()
    {
        BindCoroutineRunner();
        BindSceneLoader();
        BindPauseManager();
        BindInput();
        BindRecordService();
        BindAudioService();
    }

    private void BindCoroutineRunner()
    {
        Container.Bind<ICoroutineRunner>()
           .To<CoroutineRunner>()
           .FromNewComponentOnNewGameObject()
           .WithGameObjectName("[Global_CoroutineRunner]")
           .AsSingle()
           .NonLazy();
    }

    private void BindSceneLoader()
    {
        Container.Bind<ISceneLoader>()
               .To<SceneLoaderService>()
               .AsSingle()
               .WithArguments(_curtainPrefab)
               .NonLazy();
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
