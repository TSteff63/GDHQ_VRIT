using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetThisObject_EventReceiver : MonoBehaviour
{
    //store starting position  and rotation of this object
    [SerializeField]
    private Vector3 startingPos;
    [SerializeField]
    private Vector3 startingRot;

    //rb declaration
    Rigidbody _rigidbody;

    void Start()
    {
        //rb reference
        _rigidbody = GetComponent<Rigidbody>();

        //save the starting point of this object
        startingPos = this.transform.position;
        startingRot = this.transform.eulerAngles;

        //event to move items back to their original position
        ResetInteractables.onClick += ResetPosition;
    }

    ///  START OF EVENT METHOD  - event to move items back to their original position
    //This should also clear any velocity of the object and reset rotation as well.
    public void ResetPosition()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        this.transform.position = startingPos;
        this.transform.eulerAngles = startingRot;
    }
    ///  END OF EVENT
}
