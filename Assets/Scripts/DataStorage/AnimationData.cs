using UnityEngine;

public class AnimationData 
{
    #region Animator Hash
    public readonly int MovementInputTappedHash = Animator.StringToHash("MovementInputTapped");
    public readonly int MovementInputPressedHash = Animator.StringToHash("MovementInputPressed");
    public readonly int MmovementInputHeldHash = Animator.StringToHash("MovementInputHeld");
    public readonly int ShuffleDirectionXHash = Animator.StringToHash("ShuffleDirectionX");
    public readonly int ShuffleDirectionZHash = Animator.StringToHash("ShuffleDirectionZ");

    public readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
    public readonly int CurrentGaitHash = Animator.StringToHash("CurrentGait");

    public readonly int IsJumpingAnimHash = Animator.StringToHash("IsJumping");
    public readonly int FallingDurationHash = Animator.StringToHash("FallingDuration");

    public readonly int InclineAngleHash = Animator.StringToHash("InclineAngle");

    public readonly int StrafeDirectionXHash = Animator.StringToHash("StrafeDirectionX");
    public readonly int StrafeDirectionZHash = Animator.StringToHash("StrafeDirectionZ");

    public readonly int ForwardStrafeHash = Animator.StringToHash("ForwardStrafe");
    public readonly int CameraRotationOffsetHash = Animator.StringToHash("CameraRotationOffset");
    public readonly int IsStrafingHash = Animator.StringToHash("IsStrafing");
    public readonly int IsTurningInPlaceHash = Animator.StringToHash("IsTurningInPlace");

    public readonly int IsCrouchingHash = Animator.StringToHash("IsCrouching");

    public readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    public readonly int IsStoppedHash = Animator.StringToHash("IsStopped");
    public readonly int IsStartingHash = Animator.StringToHash("IsStarting");

    public readonly int IsGroundedHash = Animator.StringToHash("IsGrounded");

    public readonly int LeanValueHash = Animator.StringToHash("LeanValue");
    public readonly int HeadLookXHash = Animator.StringToHash("HeadLookX");
    public readonly int HeadLookYHash = Animator.StringToHash("HeadLookY");

    public readonly int BodyLookXHash = Animator.StringToHash("BodyLookX");
    public readonly int BodyLookYHash = Animator.StringToHash("BodyLookY");

    public readonly int LocomotionStartDirectionHash = Animator.StringToHash("LocomotionStartDirection");
    #endregion

    public bool MovementInputTapped;
    public bool MovementInputPressed;
    public bool MovementInputHeld;
    public bool IsStraffing;
    public bool IsCrouching;
    public bool IsStopped;
    public bool IsJumping;
    public bool IsGrounded;

    public float ShuffleDirectionX;
    public float ShuffleDirectionZ;
    public float MoveSpeed;
    public int CurrentGait;

    public float FallDuration;
    public float InclineAngle;

    public bool isWalking;
    public bool isStarting;

    public float LeanValue;
    public float HeadLookX;
    public float HeadLookY;
    public float BodyLookX;
    public float BodyLookY;

    public float LocomotionStartDirection;


}
