using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSkybox : MonoBehaviour
{
    [SerializeField]
    private Material[] skyboxes;


    public void ChangeSkybox(int i)
    {
        RenderSettings.skybox = skyboxes[i];
    }
}
