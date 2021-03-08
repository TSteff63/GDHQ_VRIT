using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we want this button to trigger on, then turn back off with a delay, during the on time, it will instantiate coffee gameobjects at a spawnpoint
public class MyButton_PushTrigger_Derived : MyButton_AbstractParent
{
    //Event Senders
    public delegate void ButtonCount();
    public static event ButtonCount TopConsoleCountIncrease;
    public static event ButtonCount BtmConsoleCountIncrease;
    public static event ButtonCount TopConsoleCountDecrease;
    public static event ButtonCount BtmConsoleCountDecrease;
    public static event ButtonCount ReactorCountIncrease;
    public static event ButtonCount ReactorCountDecrease;

    [SerializeField]
    private bool isBottomConsoleButton;

    [SerializeField]
    private bool isReactorBtn;


    [SerializeField]
    private bool sendCountEvent;


    //increases the current count
    protected override void OnLogic()
    {
        base.OnLogic();
        if((buttonOn) && (sendCountEvent))
        {
            //bottom console
            if (isBottomConsoleButton)
            {
                if (BtmConsoleCountIncrease != null)
                {
                    Debug.Log("Count increased");
                    BtmConsoleCountIncrease();
                }
            }
            //top console
            if ((!isBottomConsoleButton) && (!isReactorBtn))
            {
                if (TopConsoleCountIncrease != null)
                {
                    Debug.Log("Count increased");
                    TopConsoleCountIncrease();
                }
            }
            //reactor buttons
            if(isReactorBtn)
            {
                if (ReactorCountIncrease != null)
                {
                    Debug.Log("Count increased");
                    ReactorCountIncrease();
                }
            }
        }
    }

    //Lowers the current count
    protected override void OffLogic()
    {
        base.OffLogic();
        if ((!buttonOn) && (sendCountEvent))
        {
            //bottom console
            if (isBottomConsoleButton)
            {
                if (BtmConsoleCountDecrease != null)
                {
                    Debug.Log("Count decreased");
                    BtmConsoleCountDecrease();
                }
            }
            //top console
            if ((!isBottomConsoleButton) && (!isReactorBtn))
            {
                if (TopConsoleCountDecrease != null)
                {
                    Debug.Log("Count decreased");
                    TopConsoleCountDecrease();
                }
            }
            //reactor buttons
            if (isReactorBtn)
            {
                if (ReactorCountIncrease != null)
                {
                    Debug.Log("Count increased");
                    ReactorCountIncrease();
                }
            }
        }
    }
}