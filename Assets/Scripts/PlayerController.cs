using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [Header("Shared Values")]
    [SerializeField] MovementStates currentState;
    [SerializeField] Vector2 moveInput;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isIdle;
    [Tooltip("0 = false | 1 = true")]
    [SerializeField] bool isSprinting;
    [SerializeField] bool isJumping;
    [SerializeField] bool isGrounded;
    
    private MovementSystem movementSystem;
    private AnimationSystem animationSystem;
    private InputSystem inputSystem;

    public CharacterController CharacterController;
    public Animator Animator;

    [SerializeField] bool movementInputDetected;
    [SerializeField] float movementInputDuration;


    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();

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
        // Get input data
        moveInput = inputSystem.MoveInput;
        Vector2 lookInput = inputSystem.LookInput;
        isJumping = inputSystem.IsJumping;
        isSprinting = inputSystem.IsSprinting;

        // Pass input to the MovementSystem
        movementSystem.MoveInput = moveInput;
        movementSystem.IsSprinting = isSprinting;

        //Calculate whether the player is falling
        isGrounded = movementSystem.IsGrounded();

        // Pass input data to systems
        currentState = movementSystem.CurrentState;
        movementSystem.UpdateState(moveInput);

        //Retrieve new data from MovementSystem
        movementSystem.IsSprinting = isSprinting;
        moveSpeed = movementSystem.currentSpeed;
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
