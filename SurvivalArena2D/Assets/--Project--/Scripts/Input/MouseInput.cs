using UnityEngine;
using Zenject;

public class MouseInput : IMouseInput, ITickable
{
    private Camera _camera;
    public Vector2 MouseWorldPosition { get; private set; }
    [Inject]
    private void Construct(Camera camera)
    {
        _camera = camera;
    }
    public void Tick()
    {
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = -_camera.transform.position.z;
        MouseWorldPosition = _camera.ScreenToWorldPoint(mouseScreen);
    }
}
