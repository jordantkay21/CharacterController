using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private MovementSystem movementSystem;
    private AnimationSystem animationSystem;
    private InputSystem inputSystem;

    public CharacterController CharacterController { get; private set; }
    public Animator Animator { get; private set; }

    [SerializeField] bool movementInputDetected;
    [SerializeField] float movementInputDuration;


    private void Awake()
    {
        movementSystem = new MovementSystem(CharacterController);
        animationSystem = new AnimationSystem();
        inputSystem = new InputSystem();

        //Subscribe to events
        inputSystem.onSprintActivated += HandleSprintStart;
        inputSystem.onSprintDeactivated += HandleSprintStop;

        inputSystem.onCrouchActivated += HandleCrouchStart;
        inputSystem.onCrouchDeactivated += HandleCrouchStop;

        inputSystem.onAimActivated += HandleAimStart;
        inputSystem.onAimDeactivated += HandleAimStop;
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
        Vector2 moveInput = inputSystem.MoveInput;
        Vector2 lookInput = inputSystem.LookInput;
        bool isJumping = inputSystem.IsJumping;
        bool isSprinting = inputSystem.IsSprinting;

        // Pass input to the MovementSystem
        movementSystem.MoveInput = moveInput;
        movementSystem.IsSprinting = isSprinting;

        // Pass input data to systems
        movementSystem.ProcessMovement(moveInput, isJumping);
        animationSystem.UpdateAnimation(moveInput);

        //Handle looking and aiming
        if (inputSystem.IsAiming)
        {
            //Handle aiming logic
        }
    }

    #region EventHandlers

    private void HandleSprintStart()
    {
        //Handle sprint start logic
    }

    private void HandleSprintStop()
    {
        //Handle Sprint stop logic
    }

    private void HandleCrouchStart()
    {
        //Handle crouch start logic
    }

    private void HandleCrouchStop()
    {
        //Handle crouch stop logic
    }

    private void HandleAimStart()
    {
        //Handle aim start logic
    }

    private void HandleAimStop()
    {
        //Handle aim stop logic
    }

    #endregion

}
