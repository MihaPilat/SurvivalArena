using UnityEngine;
using Zenject;

public class GameStartControllerInstaller : MonoInstaller
{
    [SerializeField] private ExplosionEffect _explosionEffectPrefab;
    [SerializeField] private StepDustEffect _stepDustEffect;

    public override void InstallBindings()
    {
        Container.BindInstance(_explosionEffectPrefab).AsSingle();

        Container.BindInstance(_stepDustEffect).AsSingle();

        Container.BindInterfacesTo<GameStartController>().AsSingle().NonLazy();
    }
}
