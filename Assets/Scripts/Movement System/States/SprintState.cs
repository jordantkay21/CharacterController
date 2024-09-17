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
        movementSystem.ApplyMovement(movementSystem.MoveInput, movementSystem.SprintSpeed);
        RotateCharacterBasedOnInput();
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

    private void RotateCharacterBasedOnInput()
    {
         // Convert input to a direction vector in world space
        Vector3 direction = new Vector3(movementSystem.MoveInput.x, 0, movementSystem.MoveInput.y);

        if (direction.sqrMagnitude > 0.01f)  // Avoid unnecessary rotations when no input
        {
            // Calculate the target rotation based on the input direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the character towards the target direction
            movementSystem.characterController.transform.rotation = Quaternion.Slerp(
                movementSystem.characterController.transform.rotation, 
                targetRotation, 
                Time.deltaTime * rotationSpeed);
        }
    }
}

