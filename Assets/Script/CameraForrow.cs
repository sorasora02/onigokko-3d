using UnityEngine;
using UnityEngine.InputSystem;

public class CameraForrow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 4f, -8f);
    public float followSpeed = 10f;
    public float mouseSensitivity = 0.12f;
    public float minPitch = 10f;
    public float maxPitch = 60f;

    private float yaw;
    private float pitch = 25f;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        ReadMouseLook();

        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = target.position + cameraRotation * offset;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime
        );

        transform.LookAt(target.position + Vector3.up * 1.2f);
    }

    private void ReadMouseLook()
    {
        if (Mouse.current == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        yaw += mouseDelta.x * mouseSensitivity;
        pitch += mouseDelta.y * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
    }
}
