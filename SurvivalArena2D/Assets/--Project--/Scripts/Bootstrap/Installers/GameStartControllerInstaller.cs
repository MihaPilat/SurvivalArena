using UnityEngine;
using Zenject;

public class GameStartControllerInstaller : MonoInstaller
{
    [SerializeField] private ExplosionEffect _explosionEffectPrefab;

    public override void InstallBindings()
    {
        Container.Bind<PoolFactory>().AsSingle();

        Container.BindInstance(_explosionEffectPrefab).AsSingle();

        Container.BindInterfacesTo<GameStartController>().AsSingle().NonLazy();
    }
}
