using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LevelGenerator levelGenerator = (LevelGenerator)target;
        if (GUILayout.Button("Generate Level"))
        {
            levelGenerator.GenerateLayout();
        }

        if (GUILayout.Button("Clear"))
        {
            levelGenerator.Clear();
        }
    }
}
