using System;
using UnityEngine;

/// <summary>
/// A system that handles character movement and state transitions.
/// </summary>
public class MovementSystem
{
    private CharacterController characterController;
    private MovementData moveData;

    /// <summary>
    /// Initializes a new instance of the MovementSystem class with the specified CharacterController.
    /// </summary>
    /// <param name="characterController">CharacterController Component of the Character</param>
    public MovementSystem(CharacterController characterController, MovementData movementData)
    {
        this.characterController = characterController;
        this.moveData = movementData;
    }

    public void UpdateMovementState()
    {
        if (moveData.IsStopped)
        {
            moveData.CurrentState = MovementStates.Base;
            moveData.CurrentGait = MovementGait.Idle;
            moveData.TargetSpeed = 0;
        }
    }






    #region Archieved Code

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

    /// <summary>
    /// Updates the movement system based on input and camera forward direction
    /// </summary>
    /// <param name="input">The movement input vector</param>
    /// <param name="sprintInput">Indicates whether the sprint input is active</param>
    /// <param name="cameraForwardInput">The forward direction of the camera</param>
    public void UpdateState()
    {
        StateManagment();
        CheckGround();
        ApplyGravity();
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

            if (isJumping)
                Jump();
        }
        else
        {
            if (isSprinting)
            {
                CurrentState = MovementStates.Sprinting;
                CurrentSpeed = 2;
                
                if (isJumping)
                    Jump();
            }
            else
            {
                CurrentState = MovementStates.Running;
                CurrentSpeed = 1;

                if (isJumping)
                    Jump();
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
            debugColor = Color.green;
            if (isJumping)
            {
                return;
            }
            else
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
            debugColor = Color.red;
        }

        //Draw the ray in teh scene view for debugging
        Debug.DrawRay(rayStart, rayDirection * groundCheckDistance, debugColor);
    }

    public void Jump()
    {
        if (isGrounded)
        {

            //Apply jump velocity using the formula for jump height
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
            isGrounded = false;
        }

    }

    public void ApplyGravity()
    {
        //Apply gravity only when not grounded or jumping
        if (!isGrounded && !isJumping)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //Apply the vertical velocity to the character
        characterController.Move(velocity * Time.deltaTime);
    }

    #endregion
}
