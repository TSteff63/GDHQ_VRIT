using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ResetInteractables))]
public class ResetBalls : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ResetInteractables myScript = (ResetInteractables)target;
        if (GUILayout.Button("Reset Balls"))
        {
            myScript.Reset();
        }
    }
}
