using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoosterConfig))]
public class BoosterConfig_Editor : EffectorConfig_Editor
{
    private SerializedProperty _boosterProp;

    private void OnEnable()
    {
        base.OnEnable();
        _boosterProp = serializedObject.FindProperty("_prefab");
    }

    protected override void AdditionalInspectorGUI()
    {
        EditorGUILayout.PropertyField(_boosterProp, true);
    }
}
