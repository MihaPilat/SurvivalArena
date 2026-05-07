using UnityEngine;
using Zenject;

public class WaveHandlerInstaller : MonoInstaller
{
    [SerializeField] private EnemySpawner _enemySpawner;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EnemySpawner>()
            .FromComponentInHierarchy(_enemySpawner)
            .AsSingle();
    }
}
