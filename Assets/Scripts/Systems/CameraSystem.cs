using UnityEngine;
using Unity.Cinemachine;

public class CameraSystem
{
    private Camera _mainCamera;

    public CameraSystem(Camera camera)
    {
        _mainCamera = camera;
    }
    /// <summary>
    /// Gets the position of the camera.
    /// </summary>
    /// <returns>The position of the camera.</returns>
    public Vector3 GetCameraPosition()
    {
        return _mainCamera.transform.position;
    }

    /// <summary>
    /// Gets the forward vector of the camera.
    /// </summary>
    /// <returns>The forward vector of the camera.</returns>
    public Vector3 GetCameraForward()
    {
        return _mainCamera.transform.forward;
    }
    /// <summary>
    /// Gets the forward vector of the camera with the Y value zeroed.
    /// </summary>
    /// <returns>The forward vector of the camera with the Y value zeroed.</returns>
    public Vector3 GetCameraForwardZeroedY()
    {
        return new Vector3(_mainCamera.transform.forward.x, 0, _mainCamera.transform.forward.z);
    }

    /// <summary>
    /// Gets the normalised forward vector of the camera with the Y value zeroed.
    /// </summary>
    /// <returns>The normalised forward vector of the camera with the Y value zeroed.</returns>
    public Vector3 GetCameraForwardZeroedYNormalised()
    {
        return GetCameraForwardZeroedY().normalized;
    }

    /// <summary>
    /// Gets the right vector of the camera with the Y value zeroed.
    /// </summary>
    /// <returns>The right vector of the camera with the Y value zeroed.</returns>
    public Vector3 GetCameraRightZeroedY()
    {
        return new Vector3(_mainCamera.transform.right.x, 0, _mainCamera.transform.right.z);
    }
    /// <summary>
    /// Gets the normalised right vector of the camera with the Y value zeroed.
    /// </summary>
    /// <returns>The normalised right vector of the camera with the Y value zeroed.</returns>
    public Vector3 GetCameraRightZeroedYNormalised()
    {
        return GetCameraRightZeroedY().normalized;
    }
    //Method to get the current rotation of the camera (if you need the full rotation)
    public Quaternion GetCameraRotation()
    {
        return _mainCamera.transform.rotation;
    }

    /// <summary>
    /// Gets the X value of the camera tilt.
    /// </summary>
    /// <returns>The X value of the camera tilt.</returns>
    public float GetCameraTiltX()
    {
        return _mainCamera.transform.eulerAngles.x;
    }
}
