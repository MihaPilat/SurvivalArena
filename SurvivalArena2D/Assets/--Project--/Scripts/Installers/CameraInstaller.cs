using System;
using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    public override void InstallBindings()
    {
        BindMainCamera();
        BindVirtualCamera();
    }

    private void BindVirtualCamera()
    {
        Container.BindInstance(_virtualCamera).AsSingle();

        Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
    }

    private void BindMainCamera()
    {
        Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle().NonLazy();
    }
}
