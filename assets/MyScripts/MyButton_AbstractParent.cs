﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyButton_AbstractParent : MonoBehaviour
{
    /*
     * Abstract classes for buttons
        Button Challenge - Should know if button is on or off, if on turn green, if off, mesh renderer turns red...
        Delay between how fast the button can be pressed
*/

    /// <summary>  Abstract classes
    ///   Public: Can be seen and changed by any other class.
    ///  Private: Can be seen and changed only within the class it was created.
    ///Protected: Can be seen and changed within the class it was created, and any derived classes.
    /// </summary>
    /// 

    [SerializeField]
    protected bool buttonOn = false;
    [SerializeField]
    protected bool _interactable;
    [SerializeField]
    protected bool _flashing;

    [SerializeField]
    protected Animator anim;
    protected MeshRenderer _meshRender;

    [SerializeField]
    protected float buttonDelayTime;


    protected virtual void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }

        _meshRender = GetComponent<MeshRenderer>();
    }

    //if button is interactable AND it colliders with PlayerPointer, then it can be switched ON and OFF...
    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((_interactable) && (other.tag == "PlayerPointer"))
        {
            //if running while loop for flashing, end it
            if (_flashing)
            {
                _flashing = false;
            }

            //Start method to press / release button
            StartCoroutine("DelaySwitch");
        }
    }

    //IEnumerator that creates a delay for when the button can be activated again between clicks.
    protected virtual IEnumerator DelaySwitch()
    {
        //during button press, it cannot be activated again until coroutine is over
        _interactable = false;
        anim.SetTrigger("Pressed");

        yield return new WaitForSeconds(buttonDelayTime);
        if (buttonOn)
        {
            OffLogic();
        }
        else if (!buttonOn)
        {
            OnLogic();
        }
    }

    //Logic for when the button is turned ON...
    //change color, set buttonOn true, allow button to be pressed again
    protected virtual void OnLogic()
    {
        _meshRender.material.color = Color.green;
        buttonOn = true;
        _interactable = true;
    }

    //Logic for when the button is turned OFF
    //change color, set buttonOn false, allow button to be pressed again
    protected virtual void OffLogic()
    {
        _meshRender.material.color = Color.red;
        buttonOn = false;
        _interactable = true;
    }



    //public method that will swap colors on a loop
    public virtual void ActivateBtnFlash()
    {
        _flashing = true;
        StartCoroutine(FlashingColors());
    }

    //if we need to reset buttons or stop flashing for any reason, we can call this method
    public virtual void DisableBtnFlash()
    {
        _flashing = false;
    }

    //swap color based on yield return new WaitForSeconds value until the button is pressed or disabled by another method
    protected virtual IEnumerator FlashingColors()
    {
        while(_flashing)
        {
            _meshRender.material.color = Color.red;
            yield return new WaitForSeconds(0.33f);
            _meshRender.material.color = Color.black;
            yield return new WaitForSeconds(0.33f);
        }
    }
}