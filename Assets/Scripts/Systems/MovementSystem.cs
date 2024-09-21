using System;
using UnityEngine;

/// <summary>
/// A system that handles character movement and state transitions.
/// </summary>
public class MovementSystem
{
    private CharacterController characterController;
    private MovementData moveData;

    private Vector3 _velocity;
    private Vector3 _targetVelocity;
    private const float _ANIMATION_DAMP_TIME = 5f;
    private const float _STRAFE_DIRECTION_DAMP_TIME = 20f;
    private float _strafeAngle;
    private float _forwardStrafeMinThreshold = -55.0f;
    private float _forwardStrafeMaxThreshold = 125.0f;
    private float _rotationSmoothing = 10f;

    /// <summary>
    /// Initializes a new instance of the MovementSystem class with the specified CharacterController.
    /// </summary>
    /// <param name="characterController">CharacterController Component of the Character</param>
    public MovementSystem(CharacterController characterController, MovementData movementData)
    {
        this.characterController = characterController;
        this.moveData = movementData;
    }

    public void UpdateMovementSystem()
    {
        ConfigureState();
        FaceMoveDirection();
        CalculateCurrentSpeed();
    }

    public void ConfigureState()
    {
        if (moveData.MoveInput == Vector2.zero)
        {
            moveData.CurrentState = MovementStates.Base;
            moveData.TargetSpeed = 0;
        }
        else if(moveData.MoveInput.magnitude > 0)
        {
            moveData.CurrentState = MovementStates.Locomotion;
            moveData.TargetSpeed = moveData.RunSpeed;

            if (moveData.IsSprinting)
            {
                moveData.CurrentState = MovementStates.Locomotion;
                moveData.TargetSpeed = moveData.SprintSpeed;
            }
        }
    }

    private void FaceMoveDirection()
    {
        //Obtains the character's forward and right directions on the horizontal plane
        Vector3 characterForward = new Vector3(characterController.gameObject.transform.forward.x, 0f, characterController.gameObject.transform.forward.z).normalized;
        Vector3 characterRight = new Vector3(characterController.gameObject.transform.right.x, 0f, characterController.gameObject.transform.right.z).normalized;

        //Determine the movement direction based on the camera's orientation and player input
        Vector3 cameraForward = new Vector3(moveData.CameraForward.x, 0f, moveData.CameraForward.z).normalized;
        Vector3 cameraRight = new Vector3(moveData.CameraRight.x, 0f, moveData.CameraRight.z).normalized;
        Vector3 directionForward = (cameraForward * moveData.MoveInput.y + cameraRight * moveData.MoveInput.x).normalized;

        //Set the target rotation for strafing, aligning the character with the camera's forward direction
        Quaternion strafingTargetRotation = Quaternion.LookRotation(moveData.CameraForward);

        //Determine the angle between teh character's forward direction and the intended movement direction
        _strafeAngle = characterForward != directionForward ? Vector3.SignedAngle(characterForward, directionForward, Vector3.up) : 0f;
        moveData.IsTurningInPlace = false;

        if (moveData.IsStrafing)
        {
            //Character is moving while strafing
            if (moveData.MoveDirection.magnitude > 0.01)
            {

                if (moveData.CameraForward != Vector3.zero)
                {
                    // Determine the direction of movement for animation blending
                    moveData.ShuffleDirectionZ = Vector3.Dot(characterForward, directionForward);
                    moveData.ShuffleDirectionX = Vector3.Dot(characterRight, directionForward);

                    UpdateStrafeDirection(
                        Vector3.Dot(characterForward, directionForward),
                        Vector3.Dot(characterRight, directionForward));

                    //Smoothly interpolate the camera rotation offset back to zero
                    moveData.CameraRotationOffset = Mathf.Lerp(moveData.CameraRotationOffset, 0f, _rotationSmoothing * Time.deltaTime);

                    //Adjust 'ForwardStrafe' parameter for aniamtion blending
                    float targetValue = _strafeAngle > _forwardStrafeMinThreshold && _strafeAngle < _forwardStrafeMaxThreshold ? 1f : 0f;

                    if (Mathf.Abs(moveData.ForwardStrafe - targetValue) <= 0.001f)
                    {
                        moveData.ForwardStrafe = targetValue;
                    }
                    else
                    {
                        float t = Mathf.Clamp01(_STRAFE_DIRECTION_DAMP_TIME * Time.deltaTime);
                        moveData.ForwardStrafe = Mathf.SmoothStep(moveData.ForwardStrafe, targetValue, t);
                    }



                }

                //Smoothly rotate the character towards the strafing target rotation
                characterController.gameObject.transform.rotation = Quaternion.Slerp(characterController.gameObject.transform.rotation, strafingTargetRotation, _rotationSmoothing * Time.deltaTime);
            }
            //Handle character orientation when stationary during strafing
            else
            {
                //Set default values since the charater isn't moving
                UpdateStrafeDirection(1f, 0f);

                //Calculate 'newOffset' which determines the angle difference between the character's forward direction and the camera's forward direction
                float t = 20 * Time.deltaTime;
                float newOffset = 0f;
                if (characterForward != moveData.CameraForward)
                {
                    newOffset = Vector3.SignedAngle(characterForward, moveData.CameraForward, Vector3.up);
                }

                moveData.CameraRotationOffset = Mathf.Lerp(moveData.CameraRotationOffset, newOffset, t);

                //If the offset is significant (greater than 10 degrees), the character is considered to be turning in place
                if (Mathf.Abs(moveData.CameraRotationOffset) > 10)
                {
                    moveData.IsTurningInPlace = true;
                }
            }

        }
        //Orient the character in the direction of movement when not strafing
        else
        {
            UpdateStrafeDirection(1f, 0f);
            moveData.CameraRotationOffset = Mathf.Lerp(moveData.CameraRotationOffset, 0f, _rotationSmoothing * Time.deltaTime);

            //Reset Shuffle Directions, since strafing is off default values should be used
            moveData.ShuffleDirectionZ = 1;
            moveData.ShuffleDirectionX = 0;

            //Get the Input Direction in Local Space
            //Represents the movement direction based on player input
            Vector3 inputDirection = new Vector3(moveData.MoveInput.x, 0f, moveData.MoveInput.y);


            //If not moving, the method exits early
            if(inputDirection == Vector3.zero)
            {
                return;
            }



            //Transform the Input Direction to World Space
            //Convert 'inputDirection' from local space to world space using the object's current rotation
            //'worldInputDirection is the direction the character should face in world space, considering its current orientation
            Vector3 worldInputDirection = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;

            // Only rotate the character when there is forward or backward movement
            if (Mathf.Abs(inputDirection.z) > 0.01f)
            {
                //Smoothly rotate the character to face the movement direction
                Quaternion targetRotation = Quaternion.LookRotation(worldInputDirection);

                characterController.transform.rotation = Quaternion.Slerp(
                    characterController.transform.rotation,
                    targetRotation,
                    _rotationSmoothing * Time.deltaTime);
            }

            //Else, do not rotate the character (when moving sideways)
        }
    }

    /// <summary>
    ///     Updates the strafe direction variables to those provided.
    /// </summary>
    /// <param name="TargetZ">The value to set for Z axis.</param>
    /// <param name="TargetX">The value to set for X axis.</param>
    private void UpdateStrafeDirection(float TargetZ, float TargetX)
    {
        moveData.StrafeDirectionZ = Mathf.Lerp(moveData.StrafeDirectionZ, TargetZ, _ANIMATION_DAMP_TIME * Time.deltaTime);
        moveData.StrafeDirectionX = Mathf.Lerp(moveData.StrafeDirectionX, TargetX, _ANIMATION_DAMP_TIME * Time.deltaTime);
        moveData.StrafeDirectionZ = Mathf.Round(moveData.StrafeDirectionZ * 1000f) / 1000f;
        moveData.StrafeDirectionX = Mathf.Round(moveData.StrafeDirectionX * 1000f) / 1000f;
    }

    private void CalculateCurrentSpeed()
    {
        
        moveData.CurrentSpeed = Mathf.Lerp(moveData.CurrentSpeed, moveData.TargetSpeed, moveData.SpeedChange * Time.deltaTime);

        if(moveData.CurrentSpeed <= .1f)
        {
            moveData.CurrentGait = GaitState.Idle;
            moveData.CurrentSpeed = 0;
            moveData.IsStopped = true;
        }
        else if(moveData.CurrentSpeed > 0 && moveData.CurrentSpeed < 2.5)
        {
            moveData.CurrentGait = GaitState.Walking;
            moveData.IsStopped = false;
        }
        else if(moveData.CurrentSpeed >= 2.5 && moveData.CurrentSpeed < 7)
        {
            moveData.CurrentGait = GaitState.Running;
            moveData.IsStopped = false;
        }
        else
        {
            moveData.CurrentGait = GaitState.Sprinting;
            moveData.IsStopped = false;
        }
    }
}
