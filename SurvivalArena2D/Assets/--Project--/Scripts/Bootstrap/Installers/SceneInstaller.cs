using UnityEngine;
using Zenject;
using UnityEngine.Audio;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private SoundData _levelBackgroundMusic;

    public override void InstallBindings()
    {
        Container.BindInstance(_levelBackgroundMusic).WithId("LevelMusic");

        Container.BindInterfacesTo<LevelInitializer>().AsSingle().NonLazy();
    }
}
