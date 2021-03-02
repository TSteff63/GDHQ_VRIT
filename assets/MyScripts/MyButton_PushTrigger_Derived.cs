using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//we want this button to trigger on, then turn back off with a delay, during the on time, it will instantiate coffee gameobjects at a spawnpoint
public class MyButton_PushTrigger_Derived : MyButton_AbstractParent
{
    public delegate void ButtonCount();
    public static event ButtonCount TopConsoleCountIncrease;
    public static event ButtonCount BtmConsoleCountIncrease;
    public static event ButtonCount TopConsoleCountDecrease;
    public static event ButtonCount BtmConsoleCountDecrease;

    [SerializeField]
    private bool isBottomConsoleButton;

    [SerializeField]
    private bool sendCountEvent;

    protected override void OnLogic()
    {
        base.OnLogic();
        if((buttonOn) && (sendCountEvent))
        {
            if (isBottomConsoleButton)
            {
                if (BtmConsoleCountIncrease != null)
                {
                    Debug.Log("Count increased");
                    BtmConsoleCountIncrease();
                }
            }
            if (!isBottomConsoleButton)
            {
                if (TopConsoleCountIncrease != null)
                {
                    Debug.Log("Count increased");
                    TopConsoleCountIncrease();
                }
            }
        }
    }

    protected override void OffLogic()
    {
        base.OffLogic();
        if ((!buttonOn) && (sendCountEvent))
        {
            if (isBottomConsoleButton)
            {
                if (BtmConsoleCountDecrease != null)
                {
                    Debug.Log("Count decreased");
                    BtmConsoleCountDecrease();
                }
            }
            if (!isBottomConsoleButton)
            {
                if (TopConsoleCountDecrease != null)
                {
                    Debug.Log("Count decreased");
                    TopConsoleCountDecrease();
                }
            }
        }
    }
}