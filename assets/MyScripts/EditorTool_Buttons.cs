using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorTool_Buttons : MonoBehaviour
{
    public delegate void ActionClick();
    public static event ActionClick onClick;
    //public static event ActionClick startFlashing;

    public void BtnClick()
    {
        if (onClick != null)
            onClick();
    }
}
