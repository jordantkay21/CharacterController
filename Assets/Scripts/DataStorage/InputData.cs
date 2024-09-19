using UnityEngine;

public class InputData
{
    public Vector2 LookInput;
    public Vector2 MoveInput;

    public bool IsSprinting;
    public bool IsCrouching;
    public bool IsAiming;
    public bool IsJumping;

    public bool MovementInputTapped;
    public bool MovementInputHeld;
    public float ButtonHoldThreshold;

    //Passed for debugging
    public bool InputDetected_db;
    public float InputDuration_db;

    public InputData(float buttonHoldThreshold)
    {
        ButtonHoldThreshold = buttonHoldThreshold;
    }
    public void CalculateInputData()
    {
        Debug.Log($"InputDetected: {InputDetected_db}");
        if (InputDetected_db)
        {
            if (InputDuration_db > 0 && InputDuration_db < ButtonHoldThreshold)
            {
                MovementInputTapped = true;
                MovementInputHeld = false;
            }
            else
            {
                MovementInputTapped = false;
                MovementInputHeld = true;
            }

            InputDuration_db += Time.deltaTime;
            Debug.Log($"InputDuration: {InputDuration_db}");
        }
        else
        {
            InputDuration_db = 0;
            MovementInputTapped = false;
            MovementInputHeld = false;
        }
    }
}
