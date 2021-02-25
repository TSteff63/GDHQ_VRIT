using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrabbable : OVRGrabbable
{
    //adjust the velocity of an object when releasing it (ex. throw a ball further)
    [SerializeField]
    private float throw_velocity;

    public bool _grabbed;

    public bool grabbedUsingRightHand;

    //particle system to make object glow when not being held
    [SerializeField]
    ParticleSystem FX_glow;

    //hide hands
    [SerializeField]
    private bool hideHands;


    protected override void Start()
    {
        base.Start();

        //particle effect can begin again, signifying object can be picked up
        if (FX_glow != null)
        {
            Debug.Log("play glow");
            FX_glow.Play();
        }
    }

    /// <summary>
    /// WHEN THE OBJECT IS INITIALLY GRABBED
    /// </summary>
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);

        _grabbed = true;

        if(hand.name.Contains("Right"))
        {
            grabbedUsingRightHand = true;
        }
        else if(hand.name.Contains("Left"))
        {
            grabbedUsingRightHand = false;
        }
        //CustomHandLeft (MyGrabber)
        //CustomHandRight (MyGrabber)

        //stop glowing
        if (FX_glow != null)
        {
            Debug.Log("stop glow");
            FX_glow.Clear();
            FX_glow.Stop();
        }

        //determines if hands should be hidden
        if (hideHands)
        {
            grabbedBy.GetComponentInChildren<Renderer>().enabled = false;
        }
    }


    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        //determines if hands should be hidden
        if (hideHands)
        {
            grabbedBy.GetComponentInChildren<Renderer>().enabled = true;
        }

        //throw velocity pultiplies the linear velocity of this object when letting go
        base.GrabEnd((linearVelocity * throw_velocity), angularVelocity);

        _grabbed = false;

        //particle effect can begin again, signifying object can be picked up
        if (FX_glow != null)
        {
            Debug.Log("resume glow");
            FX_glow.Play();
        }


    }
}