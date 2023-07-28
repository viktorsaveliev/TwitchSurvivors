using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyDataConfig))]
public class EnemyDataSaver : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyDataConfig scriptableObject = (EnemyDataConfig)target;
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(scriptableObject);
        }
    }
}