using UnityEngine;

public class SimpleSmoothMouseLook : MonoBehaviour
{
    Vector2 _mouseAbsolute;
    Vector2 _smoothMouse;

    private Vector3 targetDirection;
    private CameraConfig cameraConfig;
    void Start()
    {
        cameraConfig = ConfigManager.main.GetConfig("CameraConfig") as CameraConfig;
        // Set target direction to the camera's initial orientation.
        targetDirection = transform.localRotation.eulerAngles;
        if (cameraConfig.LockCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        Cursor.visible = false;
    }

    void Update()
    {
        if (cameraConfig.LockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        // Allow the script to clamp based on a desired target value.
        var targetOrientation = Quaternion.Euler(targetDirection);
        var targetCharacterOrientation = Quaternion.Euler(cameraConfig.TargetCharacterDirection);
 
        // Get raw mouse input for a cleaner reading on more sensitive mice.
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
 
        // Scale input against the sensitivity setting and multiply that against the smoothing value.
        mouseDelta = Vector2.Scale(
            mouseDelta,
            new Vector2(
                cameraConfig.Sensitivity.x * cameraConfig.Smoothing.x,
                cameraConfig.Sensitivity.y * cameraConfig.Smoothing.y
            )
        );
 
        // Interpolate mouse movement over time to apply smoothing delta.
        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / cameraConfig.Smoothing.x);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / cameraConfig.Smoothing.y);
 
        // Find the absolute mouse movement value from point zero.
        _mouseAbsolute += _smoothMouse;
 
        // Clamp and apply the local x value first, so as not to be affected by world transforms.
        if (cameraConfig.ClampInDegrees.x < 360)
            _mouseAbsolute.x = Mathf.Clamp(
                _mouseAbsolute.x, -cameraConfig.ClampInDegrees.x * 0.5f, cameraConfig.ClampInDegrees.x * 0.5f
            );
 
        // Then clamp and apply the global y value.
        if (cameraConfig.ClampInDegrees.y < 360)
            _mouseAbsolute.y = Mathf.Clamp(
                _mouseAbsolute.y, -cameraConfig.ClampInDegrees.y * 0.5f, cameraConfig.ClampInDegrees.y * 0.5f
            );
 
        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, targetOrientation * Vector3.right) * targetOrientation;
 
        var yRotation = Quaternion.AngleAxis(_mouseAbsolute.x, transform.InverseTransformDirection(Vector3.up));
        transform.localRotation *= yRotation;
    }
}