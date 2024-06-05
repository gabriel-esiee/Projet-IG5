using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ButtonVR))]
public class ButtonVRInspector : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Simulate click"))
        {
            ButtonVR script = (ButtonVR)target;
            script.onRelease.Invoke();
        }
    }
}