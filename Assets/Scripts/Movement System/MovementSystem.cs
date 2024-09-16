using System;
using UnityEngine;

public class MovementSystem
{

    //Define different states
    public IMovementState IdleState { get; private set; }
    public IMovementState RunState { get; private set; }
    public IMovementState SprintState { get; private set; }

    public IMovementState currentState;

    //Shared Variables for movement
    public Vector2 MoveInput { get; set; }
    public bool IsSprinting { get; set; }
    public bool IsFalling { get; set; }
    public float gravity = -9.81f;
    public float RunSpeed = 3f;
    public float SprintSpeed = 6f;

    private CharacterController characterController;
    private Vector3 velocity;


    public MovementSystem(CharacterController controller)
    {
        characterController = controller;

        IdleState = new IdleState();
        RunState = new RunState();
        SprintState = new SprintState();

        //Set the initial state
        currentState = IdleState;
        currentState.EnterState(this);
    }

    public void ProcessMovement(Vector2 movementInput)
    {
        throw new NotImplementedException();
    }

    public void TransitionState(IMovementState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState(this);
    }

    public void UpdateState()
    {
        //Apply Gravity
        if (characterController.isGrounded && velocity.y < 0)
            velocity.y = -2f; //Prevents sticking to the ground
        else
            velocity.y += gravity * Time.deltaTime; //Apply gravity when falling

        //Apply horizontal movement based on root motion
        characterController.Move(velocity * Time.deltaTime);

        currentState.UpdateState();
    }

    public void ApplyMovement(Vector2 input, float speed)
    {
        //Calculate movement based on input and speed
        Vector3 move = new Vector3(input.x, 0, input.y);
        characterController.Move(move * speed * Time.deltaTime);
    }

    public void HandleInput(Vector2 input)
    {
        currentState.HandleInput(input);
    }

    public bool IsGrounded()
    {
        return characterController.isGrounded;
    }
}
