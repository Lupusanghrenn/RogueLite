using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LevelGenerator levelGenerator = (LevelGenerator)target;
        if (GUILayout.Button("Generate Level"))
        {
            levelGenerator.GenerateLayout();
            UnityEditor.SceneView.RepaintAll();
        }

        if (GUILayout.Button("Clear"))
        {
            levelGenerator.ClearDebugCircles();
            UnityEditor.SceneView.RepaintAll();
        }
    }
}
