using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight_StickThrottle : MonoBehaviour
{
    [Header("Drive Enable")]
    [SerializeField]
    private bool _isAccessible;

    private float xRotation;
    private float calculatedSpeed;

    //[SerializeField]
    //Flight_StickController otherFlightStick;
    [Header("Cockpit Enable")]
    [SerializeField]
    Rigidbody _cockpitObj;

    public static Action<bool> manualFlightMode_KinematicShipItems;

    bool _grabbable;
    bool _grabbed;
    [SerializeField]
    Vector3 _grabOrigin;
    [SerializeField]
    Vector3 _controllerPos;
    OVRInput.Controller _controller;
    OVRGrabber _hand;
    private Vector3 _correctedMoved;

    //hide hands
    [SerializeField]
    private bool hideHands;

    [SerializeField]
    private float breakDistance = 0.5f;


    private void OnTriggerEnter(Collider other)
    {
        if (_isAccessible)
        {
            _hand = other.GetComponentInParent<OVRGrabber>();
            if (_hand != null)
            {
                _grabbable = true;
                _controller = _hand.ReturnController();
                StartCoroutine(GrabCheck());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isAccessible)
        {
            if (_hand != null)
            {
                _grabbable = false;
                _grabbed = false;
            }
        }
    }

    IEnumerator GrabCheck()
    {
        while ((_grabbable) && (!_grabbed))
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, _controller) > .5)
            {
                GrabStart();
                StartCoroutine(WhileGrabbed());
            }

            yield return null;
        }
    }

    IEnumerator WhileGrabbed()
    {
        while (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, _controller) > .5)
        {
            Grabbing();
            yield return null;
        }

        GrabEnd();
    }


    private void GrabStart()
    {
        _grabbed = true;
        Debug.Log("grabbed throttle");
        _grabOrigin = OVRInput.GetLocalControllerPosition(_controller);
        //_grabOrigin = _hand.transform.position;

        if (manualFlightMode_KinematicShipItems != null)
        {
            manualFlightMode_KinematicShipItems(true);
        }

        //makes it so cockpit can move
        _cockpitObj.isKinematic = false;
        //removes the cockpit from dolly tracks
        //script_MyCinemaDollyTrackScript.enabled = false;


        //determines if hands should be hidden
        if (hideHands)
        {
            _hand.GetComponentInChildren<Renderer>().enabled = false;
        }
    }

    private void Grabbing()
    {
        //_controllerPos = _hand.transform.localPosition;
        _controllerPos = OVRInput.GetLocalControllerPosition(_controller);

        var moved = _grabOrigin - _controllerPos;
        //var moved = transform.position - _controllerPos;

        xRotation += (-moved.z * 100f * Time.deltaTime);
        xRotation = Mathf.Clamp(xRotation, -50, 35);

        //restrict rotation on throttle, mathf.clamp
        //_correctedMoved.x = Mathf.Clamp(-moved.z, -.1f, .1f);
        _correctedMoved.x = xRotation;
        _correctedMoved.y = 0;
        _correctedMoved.z = 0;
        transform.localRotation = Quaternion.Euler(_correctedMoved);

        calculatedSpeed = ((xRotation + 50) * 0.5f);
    }


    public float ReturnSpeed()
    {
        return calculatedSpeed;
    }

    void GrabEnd()
    {
        _grabbed = false;

        /*
        //Would have to check if either hand is grabbing, but also if the throttle is moving ship forward still.
        if (this._grabbed == false)
        {
            //makes it so cockpit can not move
            _cockpitObj.isKinematic = true;

            Debug.Log("Send event to revert rigidbodies and layers of ship items");
            if (manualFlightMode_KinematicShipItems != null)
            {
                manualFlightMode_KinematicShipItems(false);
            }
        }
        */

        //determines if hands should be hidden
        if (hideHands)
        {
            _hand.GetComponentInChildren<Renderer>().enabled = true;
        }
    }

    public void MakeAccessible(bool boolValue)
    {
        _isAccessible = boolValue;
    }

}