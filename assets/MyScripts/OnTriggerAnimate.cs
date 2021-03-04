using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerAnimate : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    private void Start()
    {
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    public void PlayAnimation(string clip)
    {
        anim.SetTrigger(clip);
    }

}
