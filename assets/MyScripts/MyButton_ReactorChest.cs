using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton_ReactorChest : MyButton_AbstractParent
{
    //Chest grabbable parts "Handle"
    [SerializeField]
    GameObject reactorChestHandle;


    protected override void OnLogic()
    {
        base.OnLogic();

        //We won't need to interact with this any more
        _interactable = false;

        //Chest grabbable parts becomes active to grab
        reactorChestHandle.SetActive(true);
    }
}
