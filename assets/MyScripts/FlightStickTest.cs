using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightStickTest : MonoBehaviour
{

    bool _grabbable;
    bool _grabbed;
    Vector3 _grabOrigin;
    Vector3 _controllerPos;
    OVRInput.Controller _controller;
    OVRGrabber _hand;
    [SerializeField]
    //GameObject _cube;
    Vector3 _correctedMoved;

    [SerializeField]
    bool isThrottle;


    private void OnTriggerEnter(Collider other)
    {
        _hand = other.GetComponentInParent<OVRGrabber>();
        if(_hand != null)
        {
            _grabbable = true;
            _controller = _hand.ReturnController();
            StartCoroutine(GrabCheck());
        }

    }

    IEnumerator GrabCheck()
    {
        while((_grabbable) && (!_grabbed))
        {
            if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, _controller) > .5)
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

    private void OnTriggerExit(Collider other)
    {
        
    }

    private void GrabStart()
    {
        _grabbed = true;
        Debug.Log("grabbed");
        _grabOrigin = _hand.transform.position;


    }

    private void Grabbing()
    {
        _controllerPos = _hand.transform.position;

        var moved = _grabOrigin - _controllerPos;

        if (!isThrottle)
        {
            _correctedMoved.x = -moved.z;
            _correctedMoved.y = 0;
            _correctedMoved.z = moved.x;
            transform.rotation = Quaternion.Euler(_correctedMoved * 100f);
        }
        //restrict rotation on throttle, mathf.clamp
        else if (isThrottle)
        {
            _correctedMoved.x = 0;
            _correctedMoved.y = 0;
            _correctedMoved.z = moved.x;
            transform.rotation = Quaternion.Euler(_correctedMoved * 100f);
        }

    }

    void GrabEnd()
    {
//Throttle shouldn't snap back
        if (!isThrottle)
        {
            transform.eulerAngles = Vector3.zero;
        }
        _grabbed = false;
    }
}

