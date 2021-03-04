using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton_AnimateAdditionalObject : MyButton_AbstractParent
{
    [SerializeField]
    private Animator otherAnim;

    [SerializeField]
    private string animTriggerName;

    [SerializeField]
    MyButton_WarpDriveButton script_warpBtn;


    protected override void AddtionalAction()
    {
        PlayAnimation(animTriggerName);
    }

    private void PlayAnimation(string clip)
    {
        otherAnim.SetTrigger(clip);
        //tell the warp button that it should be interactable
        script_warpBtn.ActivateBtnFlash();
    }
}