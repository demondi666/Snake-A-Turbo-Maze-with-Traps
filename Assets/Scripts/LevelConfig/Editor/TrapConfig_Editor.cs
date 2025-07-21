using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrapConfig))]
public class TrapConfig_Editor : EffectorConfig_Editor
{
    private SerializedProperty _trapProp;

    private void OnEnable()
    {
        base.OnEnable();
        _trapProp = serializedObject.FindProperty("_trap");
    }

    protected override void AdditionalInspectorGUI()
    {
        EditorGUILayout.PropertyField(_trapProp, true);
    }
}
