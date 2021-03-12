using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight_ShipItemListener : MonoBehaviour
{
    [SerializeField]
    private int manualFlyLayer = 20;
        
    //layer when ship is in auto pilot / not grabbing either control stick
    private int autoPilotLayer;

    //rigidbody declaration
    Rigidbody _rb;
    //bool to store original rigidbody kinematic setting
    private bool _RBisKinematic;

    void Start()
    {
        Debug.Log("auto - " + autoPilotLayer + "manual - " + manualFlyLayer);
        //Action listener to change the ship items from kinematic and their layers to non-ship colliders.
        Flight_StickController.manualFlightMode_KinematicShipItems += ChangeRBandLayer;
        //reference rigidbody on this object
        _rb = GetComponent<Rigidbody>();
        //store what the current rigidbody kinematic setting is
        _RBisKinematic = _rb.isKinematic;
        //store what the current layer this item is on
        autoPilotLayer = this.gameObject.layer;
    }

    public void ChangeRBandLayer(bool _isGrabbed)
    {
        //change layer to non-ship collider
        //change rigidbody to kinematic
        if(_isGrabbed)
        {
            if(this.gameObject.layer != manualFlyLayer){
                this.gameObject.layer = manualFlyLayer;
            }
            if(_rb.isKinematic != true){
                _rb.isKinematic = true;
            }
        }
        //revert changes (flight stick is not grabbed)
        else
        {
            Debug.Log("Reverting Rigidbody and Layers on ship items");
            if (this.gameObject.layer != autoPilotLayer){                
                this.gameObject.layer = autoPilotLayer;
            }
            if (_rb.isKinematic != _RBisKinematic){                
                _rb.isKinematic = _RBisKinematic;
            }
        }
    }

    public void MakeKinematic()
    {
        if (_rb.isKinematic != true)
        {
            _rb.isKinematic = true;
        }
    }

    public void ReleaseKinematic()
    {
        if (_rb.isKinematic != false)
        {
            _rb.isKinematic = false;
        }
    }
}
