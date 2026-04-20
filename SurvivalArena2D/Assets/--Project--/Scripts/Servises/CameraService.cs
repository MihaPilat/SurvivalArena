using Cinemachine;
using UnityEngine;

public class CameraService
{
    private readonly CinemachineVirtualCamera _vCam;

    public CameraService(CinemachineVirtualCamera vCam)
    {
        _vCam = vCam;
    }
    public CinemachineVirtualCamera RawCamera => _vCam;

    public void SetTarget(Transform target)
    {
        _vCam.Follow = target;
    }

    public void SetFov(float fov)
    {
        _vCam.m_Lens.FieldOfView = fov;
    }

}
