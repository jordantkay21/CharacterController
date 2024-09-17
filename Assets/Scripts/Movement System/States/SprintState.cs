using UnityEngine;

public class SprintState : IMovementState
{
    private MovementSystem movementSystem;
    private float rotationSpeed = 10f;

    public void EnterState(MovementSystem movementSystem)
    {
        this.movementSystem = movementSystem;
        movementSystem.CurrentState = MovementStates.Sprinting;
        // Initialize run state (e.g., set run speed)
    }

    public void UpdateState()
    {
        // Process running logic
        //movementSystem.ApplyMovement(movementSystem.MoveInput, movementSystem.SprintSpeed);
        
    }

    public void HandleInput(Vector2 input)
    {
        if (input.sqrMagnitude == 0)
        {
            movementSystem.TransitionState(movementSystem.IdleState);
        }
        else if (!movementSystem.IsSprinting)
        {
            // Transition back to running if sprint button is released
            movementSystem.TransitionState(movementSystem.RunState);
        }
    }
    public void ExitState()
    {
    }
}

