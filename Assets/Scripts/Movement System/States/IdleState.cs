using UnityEngine;

public class IdleState : IMovementState
{
    private MovementSystem movementSystem;

    public void EnterState(MovementSystem movementSystem)
    {
        this.movementSystem = movementSystem;
        //Initialze the Idle state
    }

    public void ExitState()
    {
        // Clean up or reset logic when exiting the idle state
    }

    public void UpdateState()
    {
        // Logic to remain idle (e.g., apply gravity if needed)
    }

    public void HandleInput(Vector2 input)
    {
        if (input.magnitude > 0)
        {
            // Transition to walking state if input is detected
            movementSystem.TransitionState(movementSystem.IdleState);
        }
    }

}
