using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerVibrateControllers : MonoBehaviour
{
    public void VibrateControllers(float time)
    {
        // starts vibration on the right Touch controller
        OVRInput.SetControllerVibration(0.5f, time, OVRInput.Controller.RTouch);

        // starts vibration on the right Touch controller
        OVRInput.SetControllerVibration(0.5f, time, OVRInput.Controller.LTouch);
    }
}
