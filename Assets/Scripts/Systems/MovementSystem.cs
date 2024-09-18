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
    #region Variables
    #region Components
    public CharacterController characterController;
    #endregion

    #region Constant Values
    #endregion

    #region Configurable Values 
    //Camera Variables
    public float RotateSpeed;

    //Gravity Variables
    public float gravity;

    //Movement Variables
    public float RunSpeed;
    public float SprintSpeed;

    //Jump Variables
    public float jumpHeight;

    //Variables for ground check
    public Transform groundedRaycastStart;
    public LayerMask groundLayer;
    public float groundCheckDistance;
    #endregion

    #region Retrieved Values
    //Input System
    public Vector2 MoveInput { get; set; }
    //Camera System
    public Vector3 cameraForward; // Camera forward direction
    #endregion

    #region Processed Values
    public MovementStates CurrentState;
    public float CurrentSpeed;
    public bool isGrounded;
    public bool isSprinting;
    public bool isJumping;
    #endregion

    private Vector3 velocity; //Stores vertical velocity
    #endregion

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
    public void UpdateState()
    {
        CheckGround();
        ApplyGravity();
        StateManagment();
        HandleCameraInput(cameraForward);
        RotateCharacter();     
    }

    /// <summary>
    /// Handles player state based on input
    /// </summary>
    private void StateManagment()
    {
        //Update current state based on input
        if (MoveInput == Vector2.zero)
        {
            CurrentState = MovementStates.Idle;
            CurrentSpeed = 0f;
        }
        else
        {
            if (isSprinting)
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

    //Call this method to check if the player is grounded
    public void CheckGround()
    {
        //Raycast from the character's position downward
        RaycastHit hit;
        Vector3 rayStart = groundedRaycastStart.position;
        Vector3 rayDirection = Vector3.down;

        Color debugColor;
        //Perform the raycast to check for ground
        if(Physics.Raycast(rayStart, rayDirection, out hit, groundCheckDistance, groundLayer))
        {
            isGrounded = true;
            debugColor = Color.green;
        }
        else
        {
            isGrounded = false;
            debugColor = Color.red;
        }

        //Draw the ray in teh scene view for debugging
        Debug.DrawRay(rayStart, rayDirection * groundCheckDistance, debugColor);
    }

    public void ApplyGravity()
    {
        if (isGrounded)
        {
            velocity.y = -.05f;
        }

        //Apply gravity only when not grounded or jumping
        if (!isGrounded && !isJumping)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        Debug.Log($"Velocity.y: {velocity.y}");
        //Apply the vertical velocity to the character
        characterController.Move(velocity * Time.deltaTime);
    }

}
