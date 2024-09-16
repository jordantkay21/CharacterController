using System;
using UnityEngine;

public class MovementSystem
{
    private CharacterController characterController;

    //Shared Variables for movement
    public Vector2 MoveInput { get; set; }
    public bool IsSprinting { get; set; }
    public float walkSpeed = 3f;
    public float runSpeed = 6f;

    public IMovementState currentState;

    public MovementSystem(CharacterController controller)
    {
        characterController = controller;
    }

    public void ProcessMovement(Vector2 movementInput, bool isJumping)
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
        currentState.UpdateState();
    }

    public void HandleInput(Vector2 input)
    {
        currentState.HandleInput(input);
    }

    public void ApplyMovement(Vector2 input, float speed)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}
