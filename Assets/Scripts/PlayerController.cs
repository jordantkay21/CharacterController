using UnityEngine;
using Unity.Cinemachine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] bool hideCursor;

    [Header("REQUIRED COMPONENTS")]
    [SerializeField] CharacterController CharacterController;
    [SerializeField] Animator Animator;
    [SerializeField] Camera Camera;

    [Header("CONFIGURABLE VALUES")]
    [SerializeField] float _buttonHoldThreshold;
    [SerializeField] float _gravity;
    [SerializeField] LayerMask _groundedLayer;
    [SerializeField] float _groundCheckDistance;
    [SerializeField] float _rotateSpeed;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _runSpeed;
    [SerializeField] float _sprintSpeed;
    [SerializeField] float _jumpHeight;

    [Header("STATE MANAGEMENT")]
    [SerializeField] bool _isStopped;
    [SerializeField] bool _isJumping;
    [SerializeField] bool _isCrouching;
    [SerializeField] bool _isGrounded;
    [SerializeField] bool _isSprinting;

    [Header("RUNTIME VALUES")]
    [SerializeField] Vector2 _moveInput;
    [SerializeField] Vector2 _lookInput;
    [SerializeField] float _currentSpeed;
    [SerializeField] float _targetSpeed;
    [SerializeField] bool _movementInputDetected;
    [SerializeField] float _movementInputDuration;
    [SerializeField] bool _movementInputTapped;
    [SerializeField] bool _movementInputHeld;


    private AnimationData AnimData;
    private MovementData MoveData;
    private InputData InputData;

    private MovementSystem movementSystem;
    private AnimationSystem animationSystem;
    private InputSystem inputSystem;
    private CameraSystem cameraSystem;


    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        Camera = GetComponentInChildren<Camera>();

        InputData = new InputData(_buttonHoldThreshold);
        AnimData = new AnimationData();
        MoveData = new MovementData();

        cameraSystem = new CameraSystem(Camera);
        movementSystem = new MovementSystem(CharacterController, MoveData);
        animationSystem = new AnimationSystem(Animator, AnimData);
        inputSystem = new InputSystem(InputData);
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
        if (hideCursor)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        ProcessInput();
    }

    private void ProcessInput()
    {
        InputData.CalculateInputData();

        _moveInput = InputData.MoveInput;
        _lookInput = InputData.LookInput;
        _isCrouching = InputData.IsCrouching;
        _isJumping = InputData.IsJumping;
        _isSprinting = InputData.IsSprinting;
        _movementInputDetected = InputData.InputDetected_db;
        _movementInputDuration = InputData.InputDuration_db;
        _movementInputTapped = InputData.MovementInputTapped;
        _movementInputHeld = InputData.MovementInputHeld;
        

    }
    private void RetriveCameraData()
    {
        //Get camera forward direction from the CameraSystem
        cameraForward = cameraSystem.GetCameraForward();
    }
    public void OnJumpEnd(string jump)
    {
        Debug.Log($"{jump} animation has ended");
        animationSystem.OnJumpAnimationEnd();
        movementSystem.isJumping = false;
    }


    #region Archived Code
    bool movementInputDetected;
    float movementInputDuration;

    [Header("Configurable Gravity Values")]
    float gravity;

    [Header("Configurable Jump Values")]
    float jumpHeight;

    [Header("Configurable Ground Check Values")]
    Transform groundedRaycastStart;
    LayerMask groundLayer;
    float groundCheckDistance;

    [Header("Configurable Locomotion Values")]
    float rotationSpeed;

    [Header("Runtime Locomotion Values")]
    Vector2 moveInput;
    float moveSpeed;

    [Header("Runtime Camera Values")]
    Vector3 cameraForward;

    [Header("Runtime State Management")]
    MovementStates currentState;
    bool isStopped;
    bool isSprinting;
    bool isCrouching;
    bool isJumping;
    bool isGrounded;
    private void OldUpdate()
    {

       // ProcessInput();
        //RetriveCameraData();

        //PassMovementValues(); 

        //movementSystem.UpdateState();

        //RetrieveMovementData(); //Retrieves Movement data to update Inspector for debug purposes

        //animationSystem.UpdateAnimation();

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
    #endregion

}
