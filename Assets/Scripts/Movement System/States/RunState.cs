using UnityEngine;

public class RunState : IMovementState
{
    private MovementSystem movementSystem;

    public void EnterState(MovementSystem movementSystem)
    {
        this.movementSystem = movementSystem;
        movementSystem.CurrentState = MovementStates.Running;
        // Initialize walking state (e.g., set walk speed)
    }


    public void UpdateState()
    {
        // Process walking logic, e.g., move the player
        //movementSystem.ApplyMovement(movementSystem.MoveInput, movementSystem.RunSpeed);
    }

    public void HandleInput(Vector2 input)
    {
        if (input.sqrMagnitude == 0)
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
    public void ExitState()
    {
        // Clean up or reset logic when exiting the walking state
    }
}

