using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Used during the Case 4 sequence when ship is losing power and needs to enter repair mode.
/// </summary>
public class Event_ScreensOffOn : MonoBehaviour
{
    SpriteRenderer _spriteRender;

    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        GameManager.enableButtons_case4_RepairMode += EnableScreens;
        GameManager.disableButtons_case4_RepairMode += DisableScreens;
    }

    void EnableScreens()
    {
        _spriteRender.color = Color.white;
    }

    void DisableScreens()
    {
        _spriteRender.color = Color.black;
    }
}
