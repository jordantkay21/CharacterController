using System;
using UnityEngine;

public class AnimationSystem
{
    #region Animator Hash
    public readonly int MovementInputTappedHash = Animator.StringToHash("MovementInputTapped");
    public readonly int MovementInputHeldHash = Animator.StringToHash("MovementInputHeld");
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

    private Animator animator;
    private AnimationData animData;

    // Constructor to initialize the animator
    public AnimationSystem(Animator animator, AnimationData animData)
    {
        this.animator = animator;
        this.animData = animData;
    }

    public void UpdateAnimationSystem()
    {
        UpdateAnimatorController();
        RetrieveRootMotion();
    }
    // Method to update animation based on movement state
    private void UpdateAnimatorController()
    {
        //animator.SetFloat(leanValueHash, _leanValue);
        //animator.SetFloat(_headLookXHash, _headLookX);
        //animator.SetFloat(_headLookYHash, _headLookY);
        //animator.SetFloat(_bodyLookXHash, _bodyLookX);
        //animator.SetFloat(_bodyLookYHash, _bodyLookY);

        animator.SetFloat(IsStrafingHash, animData.IsStrafing ? 1.0f : 0.0f);

        animator.SetFloat(InclineAngleHash, animData.InclineAngle);

        animator.SetFloat(MoveSpeedHash, animData.MoveSpeed);
        animator.SetInteger(CurrentGaitHash, (int)animData.CurrentGait);

        animator.SetFloat(StrafeDirectionXHash, animData.StrafeDirectionX);
        animator.SetFloat(StrafeDirectionZHash, animData.StrafeDirectionZ);
        animator.SetFloat(ForwardStrafeHash, animData.ForwardStrafe);
        animator.SetFloat(CameraRotationOffsetHash, animData.CameraRotationOffset);

        animator.SetBool(MovementInputHeldHash, animData.MovementInputHeld);
        animator.SetBool(MovementInputTappedHash, animData.MovementInputTapped);
        animator.SetFloat(ShuffleDirectionXHash, animData.ShuffleDirectionX);
        animator.SetFloat(ShuffleDirectionZHash, animData.ShuffleDirectionZ);

        animator.SetBool(IsTurningInPlaceHash, animData.IsTurningInPlace);
        animator.SetBool(IsCrouchingHash, animData.IsCrouching);

        animator.SetBool(IsJumpingAnimHash, animData.IsJumping);
        //animator.SetFloat(_fallingDurationHash, _fallingDuration);
        animator.SetBool(IsGroundedHash, animData.IsGrounded);

        //animator.SetBool(_isWalkingHash, _isWalking);
        animator.SetBool(IsStoppedHash, animData.IsStopped);

        //animator.SetFloat(_locomotionStartDirectionHash, _locomotionStartDirection);
    }

    public void RetrieveRootMotion()
    {
        animData.RootMotion = animator.deltaPosition;
    }

    public void RetrieveRootVelocity()
    {
        animData.RootVelocity = animator.velocity;
    }

    public void OnJumpAnimationEnd()
    {
        //Reset isJumping to false
        animator.SetBool("IsJumping", false);
    }
}
