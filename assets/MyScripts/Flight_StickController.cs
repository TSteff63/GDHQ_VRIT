using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Flight_StickController : MonoBehaviour
    {
        //[Header("Test Purposes")]
       // public bool isNotChildOfCockpit;
       // [SerializeField]
       // private Transform stickTransform;

        [Header("Drive Enable")]
        public bool _isAccessible;

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
        public Vector3 _correctedMoved;

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
            Debug.Log("grabbed flight stick");
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

        _correctedMoved.x = -moved.z;
        _correctedMoved.y = 0;
        _correctedMoved.z = moved.x;
        transform.rotation = Quaternion.Euler(_correctedMoved * 100f);
            
        //IF BREAKDISTANCE IS 0 OR LESS, IT WILL THROW ERROR
        if (breakDistance <= 0)
            {
                breakDistance = 0.5f;
            }

            if ((Vector3.Distance(_hand.transform.position, transform.position) > breakDistance) && (breakDistance != 0))
            {
                GrabEnd();
                _grabbable = false;
            }
        }

        void GrabEnd()
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            //transform.rotation = Quaternion.identity;
            _correctedMoved = Vector3.zero;

            _grabbed = false;

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


    /*
    private void Update()
    {
        if (isNotChildOfCockpit)
        {
            this.transform.position = stickTransform.position;
        }
    }*/
}