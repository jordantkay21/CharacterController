using UnityEngine;
using Unity.Cinemachine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool showCursor;
    [SerializeField] bool movementInputDetected;
    [SerializeField] float movementInputDuration;

    [Header("Configurable Locomotion Values")]
    [SerializeField] float rotationSpeed;

    [Header("Runtime Locomotion Values")]
    [SerializeField] Vector2 moveInput;
    [SerializeField] float moveSpeed;

    [Header("Runtime Camera Values")]
    [SerializeField] Vector3 cameraForward;

    [Header("Runtime State Management")]
    [SerializeField] MovementStates currentState;
    [SerializeField] bool isIdle;
    [SerializeField] bool isSprinting;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    
    [Header("Runtime Components")]
    public CharacterController CharacterController;
    public Animator Animator;
    public Camera Camera;

    private MovementSystem movementSystem;
    private AnimationSystem animationSystem;
    private InputSystem inputSystem;
    private CameraSystem cameraSystem;




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

        RetriveInputData();
        RetriveCameraData();

        ProcessMovementData();

        // Pass the calculated variables to the AnimationSystem
        animationSystem.UpdateAnimation(isIdle, moveInput, moveSpeed, isJumping, isGrounded) ;

    }

    private void RetriveInputData()
    {
        moveInput = inputSystem.MoveInput;
        isJumping = inputSystem.IsJumping;
        isSprinting = inputSystem.IsSprinting;
    }

    private void RetriveCameraData()
    {
        //Get camera forward direction from the CameraSystem
        cameraForward = cameraSystem.GetCameraForward();
    }

    /// <summary>
    /// Inputs data retreived by Input and Camera Systems into the Movement system. 
    /// The Movement System processes said data.
    /// Processed Data is retrieved and stored.
    /// </summary>
    private void ProcessMovementData()
    {
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
    }



}
