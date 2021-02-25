using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GRABBABLE IS THE OBJECT THAT IS PICKED UP / GRABBED - EX. BASKETBALL / GUN / DOOR HANDLE / ETC.
/// </summary>
public class ConstrainedGrabbable : OVRGrabbable
{
    [SerializeField]
    private Transform _handle;
    Rigidbody _handleRB;
    private bool _grabbed;

    //hide hands
    [SerializeField]
    private bool hideHands;

    //try to get rigidbodies of children / parents / and self if != null
    //clear velocity of those objects
    [SerializeField]
    Rigidbody[] familyRigidbodies;

    //if grabbed, ignore the player's layer for collision

    //particle system to make object glow when not being held
    [SerializeField]
    ParticleSystem FX_glow;

    [SerializeField]
    private float breakDistance = 0.5f;

    protected override void Start()
    {
        base.Start();
        //get rigidbody of handle
        _handleRB = _handle.GetComponent<Rigidbody>();


        //check if this item is using a particle system
        if(GetComponentInChildren<ParticleSystem>() != null)
        {
            FX_glow = GetComponentInChildren<ParticleSystem>();
        }

        //find all rigidbodies attached to this object, it's parents, and/or children
        CreateRigidbodyArray();
    }

        //find all rigidbodies attached to this object, it's parents, and/or children
    private void CreateRigidbodyArray()
    {
        //create a list that can be used to combine / add arrays
        var myList = new List<Rigidbody>();

        //find all rigidbodies attached to parent objects only
        if (GetComponentsInParent<Rigidbody>().Length > 0)  //excludes self
        {
            for (int i = 1; i < GetComponentsInParent<Rigidbody>().Length; i++)
            {
                myList.Add(GetComponentsInParent<Rigidbody>()[i]);
            }
        }
        //find all rigidbodies attached to children and self
        if (GetComponentsInChildren<Rigidbody>() != null)  //includes self
        {
            for (int i = 0; i < GetComponentsInChildren<Rigidbody>().Length; i++)
            {
                myList.Add(GetComponentsInChildren<Rigidbody>()[i]);
            }
        }

        //check if any rigidbodies actually exist
        if (myList.Count > 0)
        {
            //add all found rigidbodies to array
            familyRigidbodies = myList.ToArray();
        }
    }



    /// <summary>
    /// WHEN THE OBJECT IS INITIALLY GRABBED
    /// </summary>
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);

        //clear velocity of the grabbed object
        if (familyRigidbodies != null)
        {
            foreach (Rigidbody rb in familyRigidbodies)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }

        StartCoroutine(UpdateHandle());

        //determines if hands should be hidden
        if (hideHands)
        {
            grabbedBy.GetComponentInChildren<Renderer>().enabled = false;
        }

        if (FX_glow != null)
        {
            FX_glow.Stop();
            FX_glow.Clear();
        }
    }

    /// <summary>
    /// WHILE THE OBJECT IS BEING HELD
    /// </summary>
    IEnumerator UpdateHandle()
    {
        _grabbed = true;
        while(_grabbed)
        {
            //move object with hand
            _handleRB.MovePosition(transform.position);
            //rotate object with hand
            //_handleRB.MoveRotation(transform.rotation);

            //IF BREAKDISTANCE IS 0 OR LESS, IT WILL THROW ERROR
            if(breakDistance <= 0)
            {
                breakDistance = 0.5f;
            }

            if ((Vector3.Distance(_handle.position, transform.position) > breakDistance) && (breakDistance != 0))
            {
                grabbedBy.ForceRelease(this);
                _grabbed = false;
            }


            yield return null;
        }
    }


    /// <summary>
    /// WHEN THE OBJECT IS RELEASED FROM THE BEING HELD
    /// </summary>
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        _grabbed = false;
        transform.position = _handle.position;
        transform.rotation = _handle.rotation;

        //if hands were hidden, then show them again...
        if (hideHands)
        {
            grabbedBy.GetComponentInChildren<Renderer>().enabled = true;
        }

        //particle effect can begin again, signifying object can be picked up
        if (FX_glow != null)
        {
            FX_glow.Play();
        }


        base.GrabEnd(linearVelocity, angularVelocity);
    }
}
