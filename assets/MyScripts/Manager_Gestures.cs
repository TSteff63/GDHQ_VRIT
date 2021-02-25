using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager_Gestures : MonoBehaviour
{
    public OVRManager script_OVRManger;

    [SerializeField]
    private TMP_Text input_R_IndTrigger, input_R_HandTrigger, input_R_A, input_R_B, input_R_ThumbStick, input_R_ThumbY, input_R_ThumbX,
        input_L_IndTrigger, input_L_HandTrigger, input_L_X, input_L_Y, input_L_ThumbStick, input_L_ThumbY, input_L_ThumbX;



    /// <summary>
    /// OVRInput Usage
    // The primary usage of OVRInput is to access controller input state through Get(), GetDown(), and GetUp().
    // Get() queries the current state of a controller.
    // GetDown() queries if a controller was pressed this frame.
    // GetUp() queries if a controller was released this frame.
    /// </summary>

    /// <summary>
    /// Control Input Enumerations
    // There are multiple variations of Get() that provide access to different sets of controls.These sets of controls are exposed through enumerations defined by OVRInput as follows:
    ///Control Enumerates
    // OVRInput.Button Traditional buttons found on gamepads, Oculus Touch controllers, and back button.
    // OVRInput.Touch Capacitive-sensitive control surfaces found on the Oculus Touch controller.
    // OVRInput.NearTouch Proximity-sensitive control surfaces found on the first generation Oculus Touch controller.Not supported on subsequent generations.
    // OVRInput.Axis1D One-dimensional controls such as triggers that report a floating point state.
    // OVRInput.Axis2D Two-dimensional controls including thumbsticks. Reports a Vector2 state.
    /// </summary>

    /// <summary>
    ///                                 Example Usage
    //      returns true if the primary button (typically “A”) is currently pressed.
    // OVRInput.Get(OVRInput.Button.One);

    //      returns true if the primary button (typically “A”) was pressed this frame.
    // OVRInput.GetDown(OVRInput.Button.One);

    //      returns true if the “X” button was released this frame.
    // OVRInput.GetUp(OVRInput.RawButton.X);

    //      returns a Vector2 of the primary (typically the Left) thumbstick’s current state.
    //      (X/Y range of -1.0f to 1.0f)
    // OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

    //      returns true if the primary thumbstick is currently pressed (clicked as a button)
    // OVRInput.Get(OVRInput.Button.PrimaryThumbstick);

    //      returns true if the primary thumbstick has been moved upwards more than halfway.
    //      (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
    // OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);

    //      returns a float of the secondary (typically the Right) index finger trigger’s current state.
    //      (range of 0.0f to 1.0f)
    // OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

    //      returns a float of the left index finger trigger’s current state.
    //      (range of 0.0f to 1.0f)
    // OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

    //      returns true if the left index finger trigger has been pressed more than halfway.
    //      (Interpret the trigger as a button).
    // OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

    //      returns true if the secondary gamepad button, typically “B”, is currently touched by the user.
    // OVRInput.Get(OVRInput.Touch.Two);
    /// </summary>
    
    
    public void Update()
    {
        OVRInput.Update();

        DisplayInputs();
    }


    private void DisplayInputs()
    {
        ///                     RIGHT CONTROLLER
        ///Display pressed buttons on ui to determine if input code functions correctly
        //if the right trigger is pressed, then the ui should display this respectively...
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.35f)
        {
            input_R_IndTrigger.text = "Held";
        }
        else
        {
            input_R_IndTrigger.text = "< Released >";
        }
        //Right Hand Trigger...
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.35f)
        {
            input_R_HandTrigger.text = "Held";
        }
        else
        {
            input_R_HandTrigger.text = "< Released >";
        }
        //A Button...
        if (OVRInput.Get(OVRInput.Button.One))
        {
            input_R_A.text = "Held";
        }
        else
        {
            input_R_A.text = "< Released >";
        }
        //B Button..
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            input_R_B.text = "Held";
        }
        else
        {
            input_R_B.text = "< Released >";
        }
        //Thumbstick Button..
        if (OVRInput.Get(OVRInput.Button.SecondaryThumbstick))
        {
            input_R_ThumbStick.text = "Held";
        }
        else
        {
            input_R_ThumbStick.text = "< Released >";
        }

        //thumbstick direction detection
        if ((OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y > 0.1) || (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y < 0.1))
        {
            input_R_ThumbY.text = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y.ToString();
        }
        else
        {
            input_R_ThumbY.text = "< Released >";
        }

        //thumbstick direction detection
        if ((OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x > 0.1) || (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x < 0.1))
        {
            input_R_ThumbX.text = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x.ToString();
        }
        else
        {
            input_R_ThumbX.text = "< Released >";
        }



        ///
        ///                     LEFT CONTROLLER
        ///
        //if the right trigger is pressed, then the ui should display this respectively...
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.35f)
        {
            input_L_IndTrigger.text = "Held";
        }
        else
        {
            input_L_IndTrigger.text = "< Released >";
        }
        //Right Hand Trigger...
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.35f)
        {
            input_L_HandTrigger.text = "Held";
        }
        else
        {
            input_L_HandTrigger.text = "< Released >";
        }
        //A Button...
        if (OVRInput.Get(OVRInput.Button.Three))
        {
            input_L_X.text = "Held";
        }
        else
        {
            input_L_X.text = "< Released >";
        }
        //B Button..
        if (OVRInput.Get(OVRInput.Button.Four))
        {
            input_L_Y.text = "Held";
        }
        else
        {
            input_L_Y.text = "< Released >";
        }
        //Thumbstick Button..
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick))
        {
            input_L_ThumbStick.text = "Held";
        }
        else
        {
            input_L_ThumbStick.text = "< Released >";
        }

        //thumbstick direction detection
        if ((OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0.1) || (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < 0.1))
        {
            input_L_ThumbY.text = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y.ToString();
        }
        else
        {
            input_L_ThumbY.text = "< Released >";
        }

        //thumbstick direction detection
        if ((OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > 0.1) || (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < 0.1))
        {
            input_L_ThumbX.text = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x.ToString();
        }
        else
        {
            input_L_ThumbX.text = "< Released >";
        }
    }
}
