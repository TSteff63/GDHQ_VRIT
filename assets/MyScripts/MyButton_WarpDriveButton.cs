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

    [SerializeField]
    private int directorSelection;


    protected override void OnLogic()
    {
        base.OnLogic();

        if (!hasAlreadyPlayed)
        {
            //Play the next timeline sequence
            ManagerTimeline.Instance.PlayFromTimelines(timelineSelection, directorSelection);

            //prevents warp sequence from replaying due to multiple button presses
            hasAlreadyPlayed = true;
        }
    }

    public void ChangeTimelineSequence(int newTimeline)
    {
        //changes the next timeline to proceed to
        timelineSelection = newTimeline;
        //allows button press to launch the next timeline sequence
        hasAlreadyPlayed = false;
    }

    public void ChangeDirector(int newDirector)
    {
        //changes the next director to proceed to, should run this method along side change timeline sequence method
        directorSelection = newDirector;
    }
}
