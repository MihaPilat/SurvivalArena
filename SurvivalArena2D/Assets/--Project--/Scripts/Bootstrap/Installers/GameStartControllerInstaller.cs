using UnityEngine;
using Zenject;

public class GameStartControllerInstaller : MonoInstaller
{
    [SerializeField] private ExplosionEffect _explosionEffectPrefab;

    public override void InstallBindings()
    {
        Container.BindInstance(_explosionEffectPrefab).AsSingle();

        Container.BindInterfacesTo<GameStartController>().AsSingle().NonLazy();
    }
}
