using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DoorConfig))]
public class DoorConfig_Editor : EffectorConfig_Editor
{
    private SerializedProperty _doorProp;
    private SerializedProperty _bonesToNextLevelProp;

    private void OnEnable()
    {
        base.OnEnable();
        _doorProp = serializedObject.FindProperty("_prefab");
        _bonesToNextLevelProp = serializedObject.FindProperty("_bonesToNextLevel");
    }

    protected override void AdditionalInspectorGUI()
    {
        DoorConfig config = (DoorConfig)target;
        EditorGUILayout.PropertyField(_doorProp, true);
        DrawIntProperty("BonesToNextLevel", _bonesToNextLevelProp, ref config._bonesToNextLevel);
    }

    private void DrawIntProperty(string label, SerializedProperty property, ref int value)
    {
        EditorGUILayout.PropertyField(property);

        if (property.intValue != value)
        {
            Undo.RecordObject(target, $"Changed {label}");
            value = property.intValue;
        }
    }
}
