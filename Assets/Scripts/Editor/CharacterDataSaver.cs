using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharacterDataConfig))]
public class CharacterDataSaver : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CharacterDataConfig scriptableObject = (CharacterDataConfig)target;
        if (GUILayout.Button("Save"))
        {
            EditorUtility.SetDirty(scriptableObject);
        }
    }
}