using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton_ThrottleButton_Derived : MyButton_AbstractParent
{
    [SerializeField]
    private bool hasAlreadyPlayed = false;

    //if this is true, when the button is On.  Scene should end and transition to credits, which will return to the main menu.
    [SerializeField]
    private bool endGame;

    protected override void OnLogic()
    {
        base.OnLogic();

        //use use event action to tell timeline to start playing launch sequence when this button is pushed on
        //This should only happen once, so if the button is turned on again, nothing will change.
        if(!hasAlreadyPlayed)
        {
            //activate thrusters sound clip
            MyAudioManager.Instance.PlaySFXClip(16);
            Debug.Log("Launch action from trigger button");
            //start launch sequence on timeline
            ManagerTimeline.Instance.PlayFromTimelines(0, 0);
            //OnPlayTimeline(0);
            //prevents launch sequence from replaying due to multiple button presses
            hasAlreadyPlayed = true;
        }
    }
}
