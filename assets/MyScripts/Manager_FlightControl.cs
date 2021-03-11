using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_FlightControl : MonoBehaviour
{
    /*
     *
Left grip will grab the left control stick
Right grip will grab the right control stick
This should be a toggle action, hands should probably be deactivated while only the flight controls become active.  Using grips again will reverse this for each hand.

Thrumbstick Left can move forward and back, and turn
Thumbstick Right can raise up and down vertically
"A" can initiallize thrusters for turbo speed
"B" can initiallize brakes to immediately stop the ship
Left Trigger can Fire left laser
Right Trigger can Fire right laser

Hold "X" to see flight controls as a ui pop-up.  deactivates when X is no longer held



        OR

Left controller push forward / tilt forward = turn down / back turns up
Right controller could control just the thrusters to move forwad or slow down to a stop by moving forward and back.
This should be a toggle action, hands should probably be deactivated while only the flight controls become active.  Using grips again will reverse this for each hand.

     * 
     * 
     */


    [Header("Movement")]
    public float speed = 3f;
    public float turnSpeed = 10f;
    public float turnSpeedModified;
    Vector3 m_EulerAngleVelocity;
    public float maxVelocity = 3f;

    [Header("Components")]
    Rigidbody m_Rigidbody;

    [Header("Movement Conditions")]
    public bool isManuallyDriving, isTurning, isManuallyTurboSpeed;
    [HideInInspector]
    public float horizontal, vertical;

    public bool canDrive;


    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        turnSpeedModified = turnSpeed;
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);



        if (canDrive)
        {
            //if moving vertical or horizontal axis above or below 0, then isManuallyDriving is true...
            isManuallyDriving = hasVerticalInput;
            isTurning = hasHorizontalInput;
            isManuallyTurboSpeed = Input.GetKey(KeyCode.LeftShift);

            Move(vertical);
            Turning(horizontal);

            Debug.Log("Vertical = " + vertical + " and horizontal = " + horizontal);
        }
    }



    //Moves our player using tank controls
    void Move(float vertical)
    {
        if (vertical > 0)
        {
            speed = 50;
        }
        else if (vertical < 0)
        {
            speed = 30;
        }

        if (isManuallyTurboSpeed)//Player IS using turbos, speed and max velocity are increased.
        {
            //moves the character...  ForceMode.Impulse is like instant velocity, with no acceleration over time...
            m_Rigidbody.AddForce(transform.forward * (speed * 2f) * vertical, ForceMode.Impulse);
            //This sets our max velocity for our movement...
            m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity.normalized * (maxVelocity * 2f), (maxVelocity * 2f));
        }
        else     //walking, not running
        {
            m_Rigidbody.AddForce(transform.forward * speed * vertical, ForceMode.Impulse);
            m_Rigidbody.velocity = Vector3.ClampMagnitude(m_Rigidbody.velocity.normalized * maxVelocity, maxVelocity);
        }

        //Insures our character stops moving when we aren't pressing vertical input...  resets velocity to 0,0,0. 
        if (!isManuallyDriving)
        {
            m_Rigidbody.velocity = Vector3.zero;
        }
    }



    //Turns our character based on horizontal input...
    void Turning(float horizontal)
    {
        m_EulerAngleVelocity = new Vector3(0, turnSpeed, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * horizontal);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);

        //Insures our character stops turning when we aren't pressing horizontal input...
        if (!isTurning)
        {
            m_Rigidbody.freezeRotation = true;
        }
    }
}
