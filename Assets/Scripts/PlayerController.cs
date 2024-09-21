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
    [SerializeField] float _speedChange;

    [Header("STATE MANAGEMENT")]
    [SerializeField] bool _isStopped;
    [SerializeField] bool _isJumping;
    [SerializeField] bool _isCrouching;
    [SerializeField] bool _isGrounded;
    [SerializeField] bool _isSprinting;

    [Header("RUNTIME VALUES")]
    [SerializeField] MovementStates _currentState;
    [SerializeField] GaitState _currentGait;
    [SerializeField] Vector2 _moveInput;
    [SerializeField] Vector2 _lookInput;
    [SerializeField] float _currentSpeed;
    [SerializeField] float _targetSpeed;
    [SerializeField] bool _movementInputDetected;
    [SerializeField] float _movementInputDuration;
    [SerializeField] bool _movementInputTapped;
    [SerializeField] bool _movementInputHeld;
    [SerializeField] bool _isStrafing;
    
    [Header("Player Controller Origin")]
    [SerializeField] Vector3 _moveDirection;
    [SerializeField] Vector3 _cameraForward;
    [SerializeField] Vector3 _cameraRight;

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

        ProcessCameraInput();
        ProcessInput();
        ProcessMovementData();
        UpdateAnimationData();
    }

    private void ProcessInput()
    {
        //UPDATE INSPECTOR
        InputData.CalculateInputData();

        _isStrafing = InputData.IsStrafing;
        _moveInput = InputData.MoveInput;
        _lookInput = InputData.LookInput;
        _isCrouching = InputData.IsCrouching;
        _isJumping = InputData.IsJumping;
        _isSprinting = InputData.IsSprinting;
        _movementInputDetected = InputData.InputDetected_db;
        _movementInputDuration = InputData.InputDuration_db;
        _movementInputTapped = InputData.MovementInputTapped;
        _movementInputHeld = InputData.MovementInputHeld;

        //UPDATE MOVEMENT SYSTEM
        MoveData.MoveInput = _moveInput;
        MoveData.IsCrouching = _isCrouching;
        MoveData.IsJumping = _isJumping;
        MoveData.IsSprinting = _isSprinting;
        MoveData.IsStrafing = _isStrafing;

    }

    private void ProcessCameraInput()
    {
        _moveDirection = (cameraSystem.GetCameraForwardZeroedYNormalised() * _moveInput.y) + (cameraSystem.GetCameraRightZeroedYNormalised() * _moveInput.x);
        _cameraForward = cameraSystem.GetCameraForwardZeroedYNormalised();
        _cameraRight = cameraSystem.GetCameraRightZeroedYNormalised();
    }

    private void ProcessMovementData()
    {
        MoveData.MoveDirection = _moveDirection;
        MoveData.CameraForward = _cameraForward;
        MoveData.CameraRight = _cameraRight;
        MoveData.RunSpeed = _runSpeed;
        MoveData.SprintSpeed = _sprintSpeed;
        MoveData.SpeedChange = _speedChange;

        movementSystem.UpdateMovementSystem();

        _currentSpeed = MoveData.CurrentSpeed;
        _targetSpeed = MoveData.TargetSpeed;
        _isStopped = MoveData.IsStopped;
        _currentState = MoveData.CurrentState;
        _currentGait = MoveData.CurrentGait;
        
    }

    public void UpdateAnimationData()
    {
        //UPDATE ANIMATION SYSTEM
        AnimData.MovementInputTapped = _movementInputTapped;
        AnimData.MovementInputHeld = _movementInputHeld;
        AnimData.IsCrouching = _isCrouching;
        AnimData.IsJumping = _isJumping;
        AnimData.IsStopped = _isStopped;
        AnimData.CurrentGait = (int)_currentGait;
        AnimData.MoveSpeed = _currentSpeed;

        AnimData.ShuffleDirectionX = MoveData.ShuffleDirectionX;
        AnimData.ShuffleDirectionZ = MoveData.ShuffleDirectionZ;
        AnimData.StrafeDirectionX = MoveData.StrafeDirectionX;
        AnimData.StrafeDirectionZ = MoveData.StrafeDirectionZ;
        AnimData.IsStrafing = MoveData.IsStrafing;
        AnimData.ForwardStrafe = MoveData.ForwardStrafe;
        AnimData.IsTurningInPlace = MoveData.IsTurningInPlace;
        AnimData.CameraRotationOffset = MoveData.CameraRotationOffset;

        //Temporary Placeholders
        AnimData.IsGrounded = true;

        animationSystem.UpdateAnimationSystem();
    }

}
