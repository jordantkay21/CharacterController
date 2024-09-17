using UnityEngine;

public class IdleState : IMovementState
{
    private MovementSystem movementSystem;

    public void EnterState(MovementSystem movementSystem)
    {
        this.movementSystem = movementSystem;
        movementSystem.CurrentState = MovementStates.Idle;
        movementSystem.CurrentSpeed = 0;
        movementSystem.IsIdle = true;
        //Initialze the Idle state
    }

    public void UpdateState()
    {
        // Logic to remain idle (e.g., apply gravity if needed)
    }

    public void HandleInput(Vector2 input)
    {
        if (input.sqrMagnitude > 0)
        {
            // Transition to walking state if input is detected
            movementSystem.TransitionState(movementSystem.RunState);
        }

    }

    public void ExitState()
    {
        movementSystem.IsIdle = false;
        // Clean up or reset logic when exiting the idle state
    }

}
