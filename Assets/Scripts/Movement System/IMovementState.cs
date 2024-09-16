using UnityEngine;

public interface IMovementState 
{
    void EnterState(MovementSystem movementSystem);
    void ExitState();
    void UpdateState();
    void HandleInput(Vector2 input);
}
