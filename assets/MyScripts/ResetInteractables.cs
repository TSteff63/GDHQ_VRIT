using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetInteractables : MonoBehaviour
{
    public delegate void ActionClick();
    public static event ActionClick onClick;

    public void Reset()
    {
        if(onClick != null)
        onClick();
    }
}
