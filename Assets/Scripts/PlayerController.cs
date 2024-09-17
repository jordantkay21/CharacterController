using UnityEngine;
using Unity.Cinemachine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Shared Values")]
    [SerializeField] bool showCursor = true;
    [SerializeField] MovementStates currentState;
    [SerializeField] Vector2 moveInput;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] bool isIdle;
    [Tooltip("0 = false | 1 = true")]
    [SerializeField] bool isSprinting;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    
    private MovementSystem movementSystem;
    private AnimationSystem animationSystem;
    private InputSystem inputSystem;
    private CameraSystem cameraSystem;

    public CharacterController CharacterController;
    public Animator Animator;
    public Camera Camera;

    [SerializeField] bool movementInputDetected;
    [SerializeField] float movementInputDuration;


    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        Camera = GetComponentInChildren<Camera>();

        cameraSystem = new CameraSystem(Camera);
        movementSystem = new MovementSystem(CharacterController);
        animationSystem = new AnimationSystem(Animator);
        inputSystem = new InputSystem();
    }
    private void OnEnable()
    {
        //Ensure input is enabled
        inputSystem.EnableInput();
    }
    private void OnDisable()
    {
        inputSystem.DisableInput();
    }

    private void Update()
    {
        if (showCursor)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Get input data
        moveInput = inputSystem.MoveInput;
        Vector2 lookInput = inputSystem.LookInput;
        isJumping = inputSystem.IsJumping;
        isSprinting = inputSystem.IsSprinting;

        //Get camera forward direction from the CameraSystem
        Vector3 cameraForward = cameraSystem.GetCameraForward();

        // Pass input to the MovementSystem
        movementSystem.MoveInput = moveInput;
        movementSystem.IsSprinting = isSprinting;
        movementSystem.RotateSpeed = rotationSpeed;

        // Pass input data to systems
        currentState = movementSystem.CurrentState;
        movementSystem.UpdateState(moveInput, isSprinting, cameraForward);

        //Retrieve new data from MovementSystem
        movementSystem.IsSprinting = isSprinting;
        moveSpeed = movementSystem.CurrentSpeed;
        isIdle = movementSystem.IsIdle;

        // Pass the calculated variables to the AnimationSystem
        animationSystem.UpdateAnimation(isIdle, moveInput, moveSpeed, isJumping, isGrounded) ;

        //Handle looking and aiming
        if (inputSystem.IsAiming)
        {
            //Handle aiming logic
        }
    }



}
