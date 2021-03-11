using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight_StickController : ConstrainedGrabbable
{
    [SerializeField]
    Flight_StickController otherFlightStick;
    [SerializeField]
    Rigidbody _cockpitObj;

    public static Action<bool> manualFlightMode_KinematicShipItems;


    /// <summary>
    /// WHEN THE OBJECT IS INITIALLY GRABBED
    /// </summary>
        //- If flying is true, then turn all rigidbody items kinematic and change layers to "non-ship colliding" layer
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);

        if (manualFlightMode_KinematicShipItems != null)
        {
            manualFlightMode_KinematicShipItems(true);
        }

        //makes it so cockpit can move
        _cockpitObj.isKinematic = false;
    }

    /// <summary>
    /// WHEN THE OBJECT IS RELEASED FROM THE BEING HELD
    /// </summary>
        //- if false, reverse this action ^
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);

        //only issue is that if one of the hands releases, then the items swap back, we need to check if BOTH hands are still grabbing...
        if ((this.isGrabbed == false) && (otherFlightStick.isGrabbed == false))
        {
            //makes it so cockpit can not move
            _cockpitObj.isKinematic = true;

            Debug.Log("Send event to revert rigidbodies and layers of ship items");
            if (manualFlightMode_KinematicShipItems != null)
            {
                manualFlightMode_KinematicShipItems(false);
            }
        }
    }
}
