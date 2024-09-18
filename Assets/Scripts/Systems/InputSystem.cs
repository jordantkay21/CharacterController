using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class InputSystem : PlayerInput.ICharacterControlsActions
{
    private PlayerInput controls;

    public Vector2 LookInput { get; private set; }
    public Vector2 MoveInput { get; private set; }

    public bool MovementInputDetected;
    public float MovementInputDuration;

    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get; private set; }
    public bool IsAiming { get; private set; }
    public bool IsJumping { get; private set; }


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
    public InputSystem()
    {
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


    /// <summary>
    ///     Defines the action to perform when the OnLook callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    /// <summary>
    ///     Defines the action to perform when the OnMove callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        MovementInputDetected = MoveInput.sqrMagnitude > 0;
    }

    /// <summary>
    ///     Defines the action to perform when the OnJump callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsJumping = true;
        }
    }

    /// <summary>
    ///     Defines the action to perform when the OnToggleWalk callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnToggleWalk(InputAction.CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        onWalkToggled?.Invoke();
    }

    /// <summary>
    ///     Defines the action to perform when the OnSprint callback is called.
    /// </summary>
    /// <param name="context">The context of the callback.</param>
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsSprinting = true;
            onSprintActivated?.Invoke();
        }
        else if (context.canceled)
        {
            IsSprinting = false;
            onSprintDeactivated?.Invoke();
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
            IsCrouching = true;
            onCrouchActivated?.Invoke();
        }
        else if (context.canceled)
        {
            IsCrouching = false;
            onCrouchDeactivated?.Invoke();
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
            IsAiming = true;
            onAimActivated?.Invoke();
        }

        if (context.canceled)
        {
            IsAiming = false;
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
}


