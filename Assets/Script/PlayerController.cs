using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -20f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 moveInput;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        Vector3 horizontalMove = GetCameraRelativeMove();

        if (horizontalMove.sqrMagnitude > 0.1f)
        {
            horizontalMove.Normalize();
            transform.forward = horizontalMove;
        }

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -1f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        Vector3 velocity = horizontalMove * speed;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    private Vector3 GetCameraRelativeMove()
    {
        if (cameraTransform == null)
        {
            return new Vector3(moveInput.x, 0f, moveInput.y);
        }

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        return cameraForward * moveInput.y + cameraRight * moveInput.x;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
