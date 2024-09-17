using System;
using UnityEngine;

public enum MovementStates
{
    Idle,
    Running,
    Sprinting,
    Crouching,
    Jumping,
    Falling
}

public class MovementSystem
{

    //Define different states
    public IMovementState IdleState { get; private set; }
    public IMovementState RunState { get; private set; }
    public IMovementState SprintState { get; private set; }

    private IMovementState _currentState;
    public MovementStates CurrentState;


    //Components
    public CharacterController characterController;


    //Shared Variables for movement
    public Vector2 MoveInput { get; set; }
    public bool IsIdle;
    [Tooltip("0 = false | 1 = true")]
    public bool IsSprinting { get; set; }
    public float currentSpeed;
    public float RunSpeed = 1f;
    public float SprintSpeed = 2f;


    //Jump & Fall Logic
    public bool IsFalling { get; set; }
    public float gravity = -9.81f;
    private Vector3 velocity;


    public MovementSystem(CharacterController controller)
    {
        characterController = controller;

        IdleState = new IdleState();
        RunState = new RunState();
        SprintState = new SprintState();

        //Set the initial state
        _currentState = IdleState;
        _currentState.EnterState(this);
    }

    public void TransitionState(IMovementState newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void UpdateState(Vector2 input)
    {
        HandleInput(input);
        ApplyGravity();
        _currentState.UpdateState();
        
    }

    private void HandleInput(Vector2 input)
    {
        _currentState.HandleInput(input);
    }

    public void ApplyMovement(Vector2 moveInput, float speed)
    {
        
        currentSpeed = speed;
        //Debug.Log($"Apply Movement Executed. MoveInput = {moveInput} | Speed = {currentSpeed}");
        //Calculate movement based on input and speed
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        characterController.Move(move * currentSpeed * Time.deltaTime);
    }


    public void ApplyGravity()
    {
        //Apply Gravity
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f; //Prevents sticking to the ground
        else
            velocity.y += gravity * Time.deltaTime; //Apply gravity when falling

        //Apply horizontal movement based on root motion
        //characterController.Move(velocity * Time.deltaTime);
    }
    public bool IsGrounded()
    {
        return characterController.isGrounded;
    }
}
