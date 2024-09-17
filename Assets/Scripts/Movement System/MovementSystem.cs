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

public class MovementSystem
{

    //Define different states
    public IMovementState IdleState { get; private set; }
    public IMovementState RunState { get; private set; }
    public IMovementState SprintState { get; private set; }

    private IMovementState _currentState;
    public MovementStates CurrentState;


    //Components
    public CharacterController characterController;


    //Shared Variables for movement
    public Vector2 MoveInput { get; set; }
    public Vector2 LookInput;
    public Vector3 cameraForward; // Camera forward direction
    public bool IsIdle;
    [Tooltip("0 = false | 1 = true")]
    public bool IsSprinting { get; set; }
    public float CurrentSpeed;
    public float RunSpeed = 1f;
    public float SprintSpeed = 2f;
    public float RotateSpeed;


    //Jump & Fall Logic
    public bool IsFalling { get; set; }
    public float gravity = -9.81f;
    private Vector3 velocity;


    public MovementSystem(CharacterController controller)
    {
        characterController = controller;

        IdleState = new IdleState();
        RunState = new RunState();
        SprintState = new SprintState();

        //Set the initial state
        _currentState = IdleState;
        _currentState.EnterState(this);
    }

    public void TransitionState(IMovementState newState)
    {
        _currentState.ExitState();
        _currentState = newState;
        _currentState.EnterState(this);
    }

    public void UpdateState(Vector2 input, bool sprintInput, Vector3 cameraForwardInput)
    {
        HandleInput(input, sprintInput);
        HandleCameraInput(cameraForwardInput);
        //ApplyGravity();
        RotateCharacter();
        _currentState.UpdateState();
        
    }

    private void HandleInput(Vector2 input, bool sprintInput)
    {
        //Debug.Log($"Handle Input executed. Is moveInput zero: {input = Vector2.zero}");
        // Set the current move input, sprinting state, and mouse delta
        MoveInput = input;
        IsSprinting = sprintInput;

        if (MoveInput == Vector2.zero)
            CurrentSpeed = 0;
        else
        {
            if (IsSprinting)
                CurrentSpeed = 2;
            else
                CurrentSpeed = 1;
        }

        //Handle input logic based on current state
        _currentState.HandleInput(input);
    }

    //Method to handle camera forward direction input
    public void HandleCameraInput(Vector3 cameraForwardInput)
    {
        cameraForward = cameraForwardInput;
    }

    public void RotateCharacter()
    {
        Debug.Log($"RotateCahracterBasedOnCamera is executed. Camera Forward's sqr mag: {cameraForward.sqrMagnitude}");
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
