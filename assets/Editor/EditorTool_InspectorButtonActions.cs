using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorTool_Buttons))]
public class EditorTool_InspectorButtonActions : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorTool_Buttons myScript = (EditorTool_Buttons)target;
        if (GUILayout.Button("Trigger Button"))
        {
            myScript.BtnClick();
        }

        GameManager gmScript = (GameManager)target;
        if (GUILayout.Button("Start Event 0 - Coffee"))
        {
            gmScript.StateMachine(0);
        }
    }
}