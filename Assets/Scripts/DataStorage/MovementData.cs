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
public enum GaitState
{
    Idle = 0,
    Walking = 1,
    Running = 2,
    Sprinting = 3
}
public class MovementData
{
    public MovementData()
    {
        ForwardStrafe = 1f;
    }

    #region DEBUG VALUES
    public MovementStates CurrentState;
    public GaitState CurrentGait;
    public float CurrentSpeed;
    public float TargetSpeed;
    public Vector3 GroundedSphere;
    #endregion


    #region SET BY:
    #region INPUT SYSTEM
    [Tooltip("SET BY: Input System")]
    public Vector2 MoveInput;
    [Tooltip("SET BY: Inputsystem")]
    public bool IsCrouching; 
    [Tooltip("SET BY: Inputsystem")]
    public bool IsJumping; 
    [Tooltip("SET BY: Inputsystem")]
    public bool IsSprinting; 
    [Tooltip("SET BY: Inputsystem")]
    public bool IsStrafing;
    #endregion

    #region INSPECTOR
    [Tooltip("SET BY: Inspector")]
    public float RunSpeed;
    [Tooltip("SET BY: Inspector")]
    public float SprintSpeed;
    [Tooltip("SET BY: Inspector")]
    public float SpeedChange;
    [Tooltip("SET BY: Inspector")]
    public float GroundedOffset;
    [Tooltip("SET BY: Inspector")]
    public LayerMask GroundLayerMask;
    [Tooltip("SET BY: Inspector")]
    public Transform RearRayPos;
    [Tooltip("SET BY: Inspector")]
    public Transform FrontRayPos;

    #endregion

    #region CAMERA SYSTEM
    [Tooltip("SET BY: Camera System")]
    public Vector3 MoveDirection;
    [Tooltip("SET BY: CameraSystem")]
    public Vector3 CameraForward;
    [Tooltip("SET BY: CameraSystem")]
    public Vector3 CameraRight;
    #endregion
    #endregion

    #region CONFIGURED FOR:

    #region AnimationSystem
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public bool IsGrounded;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public bool IsStopped;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public bool IsTurningInPlace;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public float ShuffleDirectionX;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public float ShuffleDirectionZ;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public float StrafeDirectionX;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public float StrafeDirectionZ;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public float ForwardStrafe;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public float CameraRotationOffset;
    [Tooltip("CONFIGURED FOR: AnimationSystem")]
    public float InclineAngle;
    
    #endregion
    
    #endregion
   
}
