using UnityEngine;
using Unity.Cinemachine;

public class CameraSystem
{
    private Camera mainCamera;

    public CameraSystem(Camera camera)
    {
        mainCamera = camera;
    }

    //Method to get the forward direction of the camera
    public Vector3 GetCameraForward()
    {
        return mainCamera.transform.forward;
    }

    //Method to get the current rotation of the camera (if you need the full rotation)
    public Quaternion GetCameraRotation()
    {
        return mainCamera.transform.rotation;
    }
}
