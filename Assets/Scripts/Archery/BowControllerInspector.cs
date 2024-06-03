using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BowController))]
public class BowControllerInspector : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Release arrow"))
        {
            BowController controller = (BowController)target;
            controller.ReleaseString(null);
        }
    }
}
