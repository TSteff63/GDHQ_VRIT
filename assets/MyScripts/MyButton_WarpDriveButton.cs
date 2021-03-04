using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton_WarpDriveButton : MyButton_AbstractParent
{
    [SerializeField]
    private bool hasAlreadyPlayed = false;

    [SerializeField]
    private int timelineSelection;


    protected override void OnLogic()
    {
        base.OnLogic();

        if (!hasAlreadyPlayed)
        {
            //Play the next timeline sequence
            ManagerTimeline.Instance.PlayFromTimelines(timelineSelection);

            //prevents warp sequence from replaying due to multiple button presses
            hasAlreadyPlayed = true;
        }
    }

    public void changeTimelineSequence(int newValue)
    {
        //changes the next timeline to proceed to
        timelineSelection = newValue;
        //allows button press to launch the next timeline sequence
        hasAlreadyPlayed = false;
    }
}
