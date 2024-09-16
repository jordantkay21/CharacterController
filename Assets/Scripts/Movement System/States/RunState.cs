using UnityEngine;

public class RunState : IMovementState
{
    private MovementSystem movementSystem;

    public void EnterState(MovementSystem movementSystem)
    {
        this.movementSystem = movementSystem;
        // Initialize walking state (e.g., set walk speed)
    }

    public void ExitState()
    {
        // Clean up or reset logic when exiting the walking state
    }

    public void UpdateState()
    {
        // Process walking logic, e.g., move the player
        movementSystem.ApplyMovement(movementSystem.MoveInput, movementSystem.RunSpeed);
    }

    public void HandleInput(Vector2 input)
    {
        if (input.magnitude == 0)
        {
            // Transition to idle if no input
            movementSystem.TransitionState(movementSystem.IdleState);
        }
        else if (movementSystem.IsSprinting)
        {
            // Transition to sprinting if sprint button is pressed
            movementSystem.TransitionState(movementSystem.SprintState);
        }
    }
}

