using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyButton_AbstractParent : MonoBehaviour
{
    /// <summary>  Abstract classes
    ///   Public: Can be seen and changed by any other class.
    ///  Private: Can be seen and changed only within the class it was created.
    ///Protected: Can be seen and changed within the class it was created, and any derived classes.
    /// </summary>


    //This is to identify which game state this can be called from through events
    [SerializeField]
    protected int caseID;

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
        _meshRender.material.EnableKeyword("_EMISSION");


        //event to trigger flashing buttons without needing to use VR
        //EditorTool_Buttons.onClick += RunTrigger;

        if (caseID == 0)
        {
            //start flashing, enable _isflashing
            GameManager.startFlashing_case0_Coffee += ActivateBtnFlash;
        }
        else if (caseID == 1)
        {
            GameManager.startFlashing_case1_EighteenButtons += ActivateBtnFlash;
        }
        else if (caseID == 2)
        {
            GameManager.startFlashing_case2_AutoPilot += ActivateBtnFlash;
        }

        //event to trigger flashing buttons without needing to use VR
        //EditorTool_Buttons.startFlashing += ActivateBtnFlash;
        EditorTool_Buttons.onClick += RunTrigger;
    }

    //used by event code
    private void RunTrigger()
    {
        if (_flashing)
        {
            StartCoroutine(DelaySwitch());
        }
    }



    //if button is interactable AND it colliders with PlayerPointer, then it can be switched ON and OFF...
    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((_interactable) && (other.tag == "PlayerPointer"))
        {
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

        //if running while loop for flashing, end it
        if (_flashing)
        {
            _flashing = false;
            Debug.Log("Disable flashing");
        }

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
        _meshRender.material.SetColor("_EmissionColor", _meshRender.material.color);
        buttonOn = true;
        _interactable = true;
    }

    //Logic for when the button is turned OFF
    //change color, set buttonOn false, allow button to be pressed again
    protected virtual void OffLogic()
    {
        _meshRender.material.color = Color.red;
        _meshRender.material.SetColor("_EmissionColor", _meshRender.material.color);
        buttonOn = false;
        _interactable = true;
    }



    //public method that will swap colors on a loop
    public virtual void ActivateBtnFlash()
    {
        Debug.Log("Playing Event Action");
        _flashing = true;
        StartCoroutine(FlashingColors());
    }

    //swap color based on yield return new WaitForSeconds value until the button is pressed or disabled by another method
    protected virtual IEnumerator FlashingColors()
    {
        while(_flashing)
        {
            _meshRender.material.color = Color.red;
            _meshRender.material.SetColor("_EmissionColor", _meshRender.material.color);
            yield return new WaitForSeconds(0.33f);
            _meshRender.material.color = Color.black;
            _meshRender.material.SetColor("_EmissionColor", _meshRender.material.color);
            yield return new WaitForSeconds(0.33f);
            if(!_flashing)
            {
                _meshRender.material.color = Color.green;
                _meshRender.material.SetColor("_EmissionColor", _meshRender.material.color);
            }
        }
    }
}