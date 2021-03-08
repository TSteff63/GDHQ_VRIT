using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton_TriggerChairTurnView : MyButton_AbstractParent
{
    ///
    /// When this button is pressed, it will turn the chair to 180 on the y axis rotation
    ///
    [SerializeField]
    GameObject _chair;

    [SerializeField]
    Animator _FaderAnim;

    private bool _audioPlayedAlready;

    /// It will initiate the reactor to start flashing (or this can be done during the event that enables the buttons, maybe use if gamemanaer is on proper enum
    //[SerializeField]
    //MeshRenderer ReactorCore;
    ///If you put the reactor core flashing code in here, it need the emission to be flashing

    protected override void OnLogic()
    {
        base.OnLogic();

        StartCoroutine(FadeOffset(true));

        //if audio hasn't played yet, play specific clip
        if (!_audioPlayedAlready)
        {
            MyAudioManager.Instance.PlayVOClip(11);
        }
        _audioPlayedAlready = true;
    }

    IEnumerator FadeOffset(bool isFacingBack)
    {
        _interactable = false;
        _FaderAnim.SetTrigger("Fade");
        yield return new WaitForSeconds(3);
        if (isFacingBack)
        {
            _chair.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (!isFacingBack)
        {
            _chair.transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        _FaderAnim.SetTrigger("Fade");
    }

    protected override void OffLogic()
    {
        base.OffLogic();

        StartCoroutine(FadeOffset(false));
    }

    //called by timeline
    public void ResetPosition()
    {
        StartCoroutine(FadeOffset(false));
    }
}
