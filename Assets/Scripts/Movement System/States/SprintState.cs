using UnityEngine;

public class SprintState : IMovementState
{
    private MovementSystem movementSystem;

    public void EnterState(MovementSystem movementSystem)
    {
        this.movementSystem = movementSystem;
        // Initialize run state (e.g., set run speed)
    }

    public void ExitState()
    {
        // Clean up or reset logic when exiting the run state
    }

    public void UpdateState()
    {
        // Process running logic
        movementSystem.ApplyMovement(movementSystem.MoveInput, movementSystem.RunSpeed);
    }

    public void HandleInput(Vector2 input)
    {
        if (input.magnitude == 0)
        {
            movementSystem.TransitionState(movementSystem.IdleState);
        }
        else if (!movementSystem.IsSprinting)
        {
            // Transition back to running if sprint button is released
            movementSystem.TransitionState(movementSystem.SprintState);
        }
    }
}

