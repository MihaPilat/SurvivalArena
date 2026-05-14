using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolFactoryInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PoolFactory>().AsSingle().NonLazy();
    }
}
