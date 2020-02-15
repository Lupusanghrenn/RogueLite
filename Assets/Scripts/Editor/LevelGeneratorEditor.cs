using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelGenerator levelGenerator = (LevelGenerator)target;
        if (GUILayout.Button("Debug"))
        {
            levelGenerator.Debuging();
            UnityEditor.SceneView.RepaintAll();
        }

        if (GUILayout.Button("add one room"))
        {
            levelGenerator.CreateRoom1Neighbor();
            UnityEditor.SceneView.RepaintAll();
        }
    }
}
