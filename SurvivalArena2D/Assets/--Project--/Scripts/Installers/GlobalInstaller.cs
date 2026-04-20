using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        BindInput();
    }

    private void BindInput()
    {
        Container.BindInterfacesTo<InputReader>().AsSingle().NonLazy();
    }
}
