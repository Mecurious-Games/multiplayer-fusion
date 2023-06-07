using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class PlayerJump : NetworkBehaviour
{
    [SerializeField] float JumpVelocity = 5f;
    [SerializeField] float GravityValue = -9.81f;
    [SerializeField] float Speed = 2f;
    [SerializeField] float RotationSpeed = 2f;
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    private Camera cam;
    [SerializeField] Vector3 camOffset = new Vector3(0, 1,-4);

    private CharacterController controller;
    private Vector3 velocity = Vector3.zero;
    private bool isJumpPressed;

    private void OnEnable()
    {
        jumpAction.Enable();
        moveAction.Enable();
    }

    private void OnDisable()
    {
        jumpAction.Disable();
        moveAction.Disable();
    }

    void OnValidate()
    {
        // Provide default bindings for the input actions
        if (jumpAction == null)
        {
            jumpAction = new InputAction(type: InputActionType.Button);
        }
        if (jumpAction.bindings.Count == 0)
        {
            jumpAction.AddBinding("<Keyboard>/space");
        }

        if (moveAction == null)
        {
            moveAction = new InputAction(type: InputActionType.Value);
        }
        if (moveAction.bindings.Count == 0)
        {
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow")
                .With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow")
                .With("Right", "<Keyboard>/rightArrow");
        }
    }

    public override void Spawned()
    {
        controller = GetComponent<CharacterController>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        cam.transform.SetParent(transform);
        cam.transform.localPosition = camOffset;
    }

    void Update()
    {
        if (jumpAction.triggered)
        {
            isJumpPressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!HasStateAuthority)
        {
            return;
        }

        // Apply gravity
        if (controller.isGrounded)
        {
            velocity.y = 0f;
        }
        else
        {
            velocity.y += GravityValue * Runner.DeltaTime;
        }

        // Jump input
        if (isJumpPressed && controller.isGrounded)
        {
            velocity.y = JumpVelocity;
            isJumpPressed = false;
        }

        // Read movement input
        Vector2 movement = moveAction.ReadValue<Vector2>();
        float rotationAmount = movement.x * Speed;
        transform.Rotate(Vector3.up, rotationAmount * Runner.DeltaTime * RotationSpeed);

        // Calculate movement vector relative to the player's forward direction
        Vector3 moveVector = transform.forward * movement.y * Speed;

        // Apply movement and gravity to the controller
        controller.Move((moveVector + velocity) * Runner.DeltaTime);

        // // move camera with player
        // cam.transform.position = transform.position + new Vector3(0, 1, 0);
        // cam.transform.rotation = transform.rotation;

    }
}
