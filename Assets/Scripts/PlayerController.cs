using UnityEngine;
using Unity.Cinemachine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool showCursor;
    [SerializeField] bool movementInputDetected;
    [SerializeField] float movementInputDuration;

    [Header("Configurable Gravity Values")]
    [SerializeField] float gravity;

    [Header("Configurable Jump Values")]
    [SerializeField] float jumpHeight;

    [Header("Configurable Ground Check Values")]
    [SerializeField] Transform groundedRaycastStart;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDistance;

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

        PassMovementValues(); 

        movementSystem.UpdateState();

        RetrieveMovementData(); //Retrieves Movement data to update Inspector for debug purposes

        animationSystem.UpdateAnimation(isIdle, moveInput, moveSpeed, isJumping, isGrounded);

    }

    private void RetriveInputData()
    {
        currentState = movementSystem.CurrentState;
        moveInput = inputSystem.MoveInput;
        isJumping = inputSystem.IsJumping;
        isSprinting = inputSystem.IsSprinting;

        Debug.Log($"Retrive Input data Executed. isJumping: {isJumping}");
    }

    private void RetriveCameraData()
    {
        //Get camera forward direction from the CameraSystem
        cameraForward = cameraSystem.GetCameraForward();
    }

    private void PassMovementValues()
    {
        movementSystem.MoveInput = moveInput;
        movementSystem.isSprinting = isSprinting;
        movementSystem.RotateSpeed = rotationSpeed;
        movementSystem.cameraForward = cameraForward;
        movementSystem.groundedRaycastStart = groundedRaycastStart;
        movementSystem.groundLayer = groundLayer;
        movementSystem.groundCheckDistance = groundCheckDistance;
        movementSystem.jumpHeight = jumpHeight;
        movementSystem.gravity = gravity;
        movementSystem.isJumping = isJumping;
        Debug.Log($"Pass Movement data executed. isJumping: {movementSystem.isJumping}");
    }

    /// <summary>
    /// Inputs data retreived by Input and Camera Systems into the Movement system. 
    /// The Movement System processes said data.
    /// Processed Data is retrieved and stored.
    /// </summary>
    private void RetrieveMovementData()
    {
        isSprinting = movementSystem.isSprinting;
        moveSpeed = movementSystem.CurrentSpeed;
        isGrounded = movementSystem.isGrounded;
    }

    public void OnJumpEnd(string jump)
    {
        Debug.Log($"{jump} animation has ended");
        animationSystem.OnJumpAnimationEnd();
        movementSystem.isJumping = false;
    }

}
