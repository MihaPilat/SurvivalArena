using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    public override void InstallBindings()
    {
        BindMainCamera();
    }

    private void BindMainCamera()
    {
        Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle().NonLazy();
    }
}
