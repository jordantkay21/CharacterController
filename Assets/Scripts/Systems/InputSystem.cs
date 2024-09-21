using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class InputSystem : PlayerInput.ICharacterControlsActions
{
    private PlayerInput controls;

    #region Animation Variable Hashes

    private readonly int _movementInputTappedHash = Animator.StringToHash("MovementInputTapped");
    private readonly int _movementInputPressedHash = Animator.StringToHash("MovementInputPressed");
    private readonly int _movementInputHeldHash = Animator.StringToHash("MovementInputHeld");
    private readonly int _shuffleDirectionXHash = Animator.StringToHash("ShuffleDirectionX");
    private readonly int _shuffleDirectionZHash = Animator.StringToHash("ShuffleDirectionZ");

    private readonly int _moveSpeedHash = Animator.StringToHash("MoveSpeed");
    private readonly int _currentGaitHash = Animator.StringToHash("CurrentGait");

    private readonly int _isJumpingAnimHash = Animator.StringToHash("IsJumping");
    private readonly int _fallingDurationHash = Animator.StringToHash("FallingDuration");

    private readonly int _inclineAngleHash = Animator.StringToHash("InclineAngle");

    private readonly int _strafeDirectionXHash = Animator.StringToHash("StrafeDirectionX");
    private readonly int _strafeDirectionZHash = Animator.StringToHash("StrafeDirectionZ");

    private readonly int _forwardStrafeHash = Animator.StringToHash("ForwardStrafe");
    private readonly int _cameraRotationOffsetHash = Animator.StringToHash("CameraRotationOffset");
    private readonly int _isStrafingHash = Animator.StringToHash("IsStrafing");
    private readonly int _isTurningInPlaceHash = Animator.StringToHash("IsTurningInPlace");

    private readonly int _isCrouchingHash = Animator.StringToHash("IsCrouching");

    private readonly int _isWalkingHash = Animator.StringToHash("IsWalking");
    private readonly int _isStoppedHash = Animator.StringToHash("IsStopped");
    private readonly int _isStartingHash = Animator.StringToHash("IsStarting");

    private readonly int _isGroundedHash = Animator.StringToHash("IsGrounded");

    private readonly int _leanValueHash = Animator.StringToHash("LeanValue");
    private readonly int _headLookXHash = Animator.StringToHash("HeadLookX");
    private readonly int _headLookYHash = Animator.StringToHash("HeadLookY");

    private readonly int _bodyLookXHash = Animator.StringToHash("BodyLookX");
    private readonly int _bodyLookYHash = Animator.StringToHash("BodyLookY");

    private readonly int _locomotionStartDirectionHash = Animator.StringToHash("LocomotionStartDirection");

    #endregion

    private InputData _inputData;

    //Action events for specific input triggers
    public Action onSprintActivated;
    public Action onSprintDeactivated;

    public Action onCrouchActivated;
    public Action onCrouchDeactivated;

    public Action onAimActivated;
    public Action onAimDeactivated;

    public Action onJumpPerformed;

    public Action onLockOnToggled;

    public Action onWalkToggled;

    /// <inheritdoc cref="OnEnable" />
    public InputSystem(InputData inputData)
    {
        _inputData = inputData;

        //Initialize input actions
        controls = new PlayerInput();
        controls.CharacterControls.SetCallbacks(this);

    }

    public void EnableInput()
    {
        controls.CharacterControls.Enable();
    }

    public void DisableInput()
    {
        controls.CharacterControls.Disable();
    }

    #region EventHandlers
    /// <summary>
    ///     Defines the action to perform when the OnLook callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnLook(InputAction.CallbackContext context)
    {
        _inputData.LookInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    ///     Defines the action to perform when the OnMove callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputData.MoveInput = context.ReadValue<Vector2>();
        _inputData.InputDetected_db = _inputData.MoveInput.magnitude > 0;
    }

    /// <summary>
    ///     Defines the action to perform when the OnJump callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputData.IsJumping = true;
        }

        if (context.canceled)
        {
            _inputData.IsJumping = false;
        }
    }

    /// <summary>
    ///     Defines the action to perform when the OnSprint callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            _inputData.IsSprinting = true;
            _inputData.IsStrafing = false;
        }
        else if (context.canceled)
        {
            _inputData.IsSprinting = false;
            _inputData.IsStrafing = true;
            
        }
    }

    /// <summary>
    ///     Defines the action to perform when the OnCrouch callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _inputData.IsCrouching = !_inputData.IsCrouching;
        }
    }

    /// <summary>
    ///     Defines the action to perform when the OnAim callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _inputData.IsAiming = true;
            onAimActivated?.Invoke();
        }

        if (context.canceled)
        {
            _inputData.IsAiming = false;
            onAimDeactivated?.Invoke();
        }
    }

    /// <summary>
    ///     Defines the action to perform when the OnLockOn callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnLockOn(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        onLockOnToggled?.Invoke();
        onSprintDeactivated?.Invoke();
    }

    public void OnToggleWalk(InputAction.CallbackContext context)
    {
        throw new NotImplementedException();
    }
    #endregion
}


