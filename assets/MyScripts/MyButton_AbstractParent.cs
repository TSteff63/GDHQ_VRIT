using System.Collections;
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

    protected Animator anim;
    protected MeshRenderer _meshRender;

    [SerializeField]
    protected float buttonDelayTime;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        _meshRender = GetComponent<MeshRenderer>();
    }

    //if button is interactable AND it colliders with PlayerPointer, then it can be switched ON and OFF...
    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((_interactable) && (other.tag == "PlayerPointer"))
        {
                StartCoroutine("DelaySwitch");
        }
    }

    //IEnumerator that creates a delay for when the button can be activated again between clicks.
    protected virtual IEnumerator DelaySwitch()
    {
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

    //Logic for when the button is turned ON
    protected virtual void OnLogic()
    {
        _meshRender.material.color = Color.green;
        buttonOn = true;
        _interactable = true;
    }

    //Logic for when the button is turned OFF
    protected virtual void OffLogic()
    {
        _meshRender.material.color = Color.red;
        buttonOn = false;
        _interactable = true;
    }
}