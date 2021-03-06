using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_DirectLightChange : MonoBehaviour
{
    Light _light;

    private void Awake()
    {
        _light = GetComponent<Light>();
    }

    void Start()
    {
        GameManager.enableButtons_case4_RepairMode += HighIntensity;
        GameManager.disableButtons_case4_RepairMode += LowIntensity;
    }

    private void LowIntensity()
    {
        _light.intensity = 0.15f;
    }
    private void HighIntensity()
    {
        _light.intensity = 1f;
    }
}
