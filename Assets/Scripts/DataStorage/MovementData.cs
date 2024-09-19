using UnityEngine;

public enum MovementStates
{
    Base,
    Locomotion,
    Jump,
    Fall,
    Crouch,

    // Archived Code
    Idle, 
    Sprinting,
    Running
}
public enum MovementGait
{
    Idle = 0,
    Walking = 1,
    Running = 2,
    Sprinting = 3
}
public class MovementData 
{
    #region Retrieved Data
    public Vector2 MoveInput;
    public Vector3 CameraForward;
    #endregion

    #region Inspector Configurable Values
    public float Gravity;
    public LayerMask groundedLayer;
    public float GroundCheckDistance;
    public float WalkSpeed;
    public float RunSpeed;
    public float SprintSpeed;
    public float JumpHeight;
    #endregion

    #region State Management Values
    public MovementGait CurrentGait; //Determines the current gait by evaluating input values and environmental values
    public MovementStates CurrentState; //Determines the current state by evaluating input values, calculated values, and environmental values

    public bool IsCrouching; // Set by InputSystem via PlayerController
    public bool IsJumping; // Set by InputSystem via PlayerController
    public bool IsStopped; // Set by calulating whether or not the player is actively moving
    public bool IsGrounded;
    public bool IsSprinting;
    #endregion

    #region Locomotion State Values

    public float CurrentSpeed;  //Stores the current speed set by calcualtions via the MovementSystem
    public float TargetSpeed; // Stores the target speed that the currentSpeed should smoothly transition to. Set by currentGait State
    public float RotateSpeed;

    #endregion
}
