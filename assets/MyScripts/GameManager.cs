using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum timelinePosition {case0_Coffee, case1_EighteenButtons, case2_WarpSpeed };

    //Event sender
    public delegate void ActionCase();
    public static event ActionCase startFlashing_case0_Coffee;
    public static event ActionCase startFlashing_case1_EighteenButtons;
    public static event ActionCase startFlashing_case2_AutoPilot;

    public timelinePosition enumPosition;

    public int buttonsPressed_top;
    public int buttonsPressed_btm;
    private bool reminderToPressBtnsVO_HasPlayed;

    private void Start()
    {
        StateMachine(enumPosition);

        MyButton_PushTrigger_Derived.BtmConsoleCountIncrease += IncreaseBtmButtonCount;
        MyButton_PushTrigger_Derived.BtmConsoleCountDecrease += DecreaseBtmButtonCount;

        MyButton_PushTrigger_Derived.TopConsoleCountIncrease += IncreaseTopButtonCount;
        MyButton_PushTrigger_Derived.TopConsoleCountDecrease += DecreaseTopButtonCount;
    }





    IEnumerator StartCaseZero()
    {
        MyAudioManager.Instance.PlayVOClip(0);
        yield return new WaitForSeconds(MyAudioManager.Instance._VO[0].length);

        if (startFlashing_case0_Coffee != null)
        {
            startFlashing_case0_Coffee();
        }
    }






    /// <summary>
    //behaviour for case one...
    /// </summary>
    IEnumerator StartCaseOne()
    { 
        yield return new WaitForSeconds(1);
        buttonsPressed_top = 0;
        buttonsPressed_btm = 0;
        MyAudioManager.Instance.PlayVOClip(1);

        if (startFlashing_case1_EighteenButtons != null)
        {
            startFlashing_case1_EighteenButtons();
        }
    }

    private void IncreaseTopButtonCount()
    {
        if (enumPosition == timelinePosition.case1_EighteenButtons)
        {
            buttonsPressed_top++;

            if ((buttonsPressed_btm == 10) && (buttonsPressed_top == 8))
            {
                enumPosition = timelinePosition.case2_WarpSpeed;
                StateMachine(enumPosition);
            }
        }
    }
    private void DecreaseTopButtonCount()
    {
        if (enumPosition == timelinePosition.case1_EighteenButtons)
        {
            buttonsPressed_top--;
        }
    }
    private void IncreaseBtmButtonCount()
    {
        if (enumPosition == timelinePosition.case1_EighteenButtons)
        {
            buttonsPressed_btm++;
            if ((buttonsPressed_btm == 10) && (!reminderToPressBtnsVO_HasPlayed) && (buttonsPressed_top != 8))
            {
                MyAudioManager.Instance.PlayVOClip(2);
                reminderToPressBtnsVO_HasPlayed = true;
            }

            if ((buttonsPressed_btm == 10) && (buttonsPressed_top == 8))
            {
                enumPosition = timelinePosition.case2_WarpSpeed;
                StateMachine(enumPosition);
            }
        }
    }
    private void DecreaseBtmButtonCount()
    {
        if (enumPosition == timelinePosition.case1_EighteenButtons)
        {
            buttonsPressed_btm--;
        }
    }
    /// <summary>
    /// End of Case One
    /// </summary>





    //behaviour for case one...
    IEnumerator StartCaseTwo()
    {
        yield return new WaitForSeconds(1);
        //"Press the button for Auto-Pilot"
        MyAudioManager.Instance.PlayVOClip(2);
        //Autopilot button begins flashing
        if (startFlashing_case2_AutoPilot != null)
        {
            startFlashing_case2_AutoPilot();
        }
        //Once pressed, A new sound clip will play 'launching the ship'
        //the script attached to that button should initiate a track&dolly camera animation, simulating take off...

        //Music Track begins to play
        //VO clip will play and player's ship will begin to follow a track out of the larger ship.
        //at end of track, VO clip plays instructing player to flip open a cover that reveals another button to press.  This will enable the warp drive
    }










    public void StateMachine(timelinePosition currentPosition)
    {
        switch (currentPosition)
        {
                //start of the game, Play initial audio, THEN coffee light flashes, player is prompted to push this button
            case timelinePosition.case0_Coffee:
                //StartCoroutine(StartCaseZero());
                StartCoroutine(StartCaseZeroEDITORTEST());
                break;

            case timelinePosition.case1_EighteenButtons:
                //Player has attempted to drink coffee, a new sound clip plays prompting the user to press flashing buttons
                //these buttons are on the front middle console level to the player and front middle console above player, roughly 16 buttons will be flashing
                StartCoroutine(StartCaseOne());
                break;

            case timelinePosition.case2_WarpSpeed:
                StartCoroutine(StartCaseTwo());

            //on success, another sound clip plays telling player to bress the flashing Throttle button...
            //Once pressed, A new sound clip will play 'launching the ship'
            //Music Track begins to play
            //VO clip will play and player's ship will begin to follow a track out of the larger ship.
            //at end of track, VO clip plays instructing player to flip open a cover that reveals another button to press.  This will enable the warp drive
                break;

            //            case 3:
            //warp sounds start to play and visuals begin to change
            //                break;

            default:
                break;
        }
    }








    IEnumerator StartCaseZeroEDITORTEST()
    {
        yield return new WaitForSeconds(1);
        if (startFlashing_case0_Coffee != null)
        {
            startFlashing_case0_Coffee();
        }
    }

}
