using System;
using UnityEngine;

public enum MovementStates
{
    Idle,
    Running,
    Sprinting,
    Crouching,
    Jumping,
    Falling
}

/// <summary>
/// A system that handles character movement and state transitions.
/// </summary>
public class MovementSystem
{
    //Components
    public CharacterController characterController;

    //Configurable Values 
    public float RotateSpeed;
    public float RunSpeed = 1f;
    public float SprintSpeed = 2f;

    //Retrieved Values
    public Vector2 MoveInput { get; set; }
    public Vector3 cameraForward; // Camera forward direction

    //Processed Values
    public float CurrentSpeed;
    public MovementStates CurrentState;
    public bool IsSprinting { get; set; }

    /// <summary>
    /// Initializes a new instance of the MovementSystem class with the specified CharacterController.
    /// </summary>
    /// <param name="controller">CharacterController Component of the Character</param>
    public MovementSystem(CharacterController controller)
    {
        characterController = controller;

    }

    /// <summary>
    /// Updates the movement system based on input and camera forward direction
    /// </summary>
    /// <param name="input">The movement input vector</param>
    /// <param name="sprintInput">Indicates whether the sprint input is active</param>
    /// <param name="cameraForwardInput">The forward direction of the camera</param>
    public void UpdateState(Vector2 input, bool sprintInput, Vector3 cameraForwardInput)
    {
        HandleInput(input, sprintInput);
        HandleCameraInput(cameraForwardInput);
        RotateCharacter();
        
    }

    /// <summary>
    /// Handles player input for movement and sprinting
    /// </summary>
    /// <param name="input">The movement input vector</param>
    /// <param name="sprintInput">Indicates whether the sprint input is active</param>
    private void HandleInput(Vector2 input, bool sprintInput)
    {
        //Set the current move input and sprinting state
        MoveInput = input;
        IsSprinting = sprintInput;

        //Update current speed based on input
        if (MoveInput == Vector2.zero)
        {
            CurrentState = MovementStates.Idle;
            CurrentSpeed = 0f;
        }
        else
        {
            if (IsSprinting)
            {
                CurrentState = MovementStates.Sprinting;
                CurrentSpeed = 2;
            }
            else
            {
                CurrentState = MovementStates.Running;
                CurrentSpeed = 1;
            }
        }
    }

    /// <summary>
    /// Handles the camera forward direction input
    /// </summary>
    /// <param name="cameraForwardInput">The forward direction of the camera</param>
    public void HandleCameraInput(Vector3 cameraForwardInput)
    {
        cameraForward = cameraForwardInput;
    }

    /// <summary>
    /// Rotates the character to face the camera's forward direction
    /// </summary>
    public void RotateCharacter()
    {
        
        if (cameraForward.sqrMagnitude > 0)
        {
            //Project the camera's forward vector onto the horizontal plane (Y=0)
            Vector3 flattenedForward = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            //Smoothly rotate the character towards the camera's forward direction
            Quaternion targetRotation = Quaternion.LookRotation(flattenedForward);
            characterController.transform.rotation = Quaternion.Slerp(
                characterController.transform.rotation,
                targetRotation,
                Time.deltaTime * RotateSpeed);
        }
    }

}
