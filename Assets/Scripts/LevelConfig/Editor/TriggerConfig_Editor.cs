using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(TriggerConfig))]
public class TriggerConfig_Editor : EffectorConfig_Editor
{
    private SerializedProperty _triggerProp;

    private void OnEnable()
    {
        base.OnEnable();
        _triggerProp = serializedObject.FindProperty("_trigger");
    }

    protected override void AdditionalInspectorGUI()
    {
        EditorGUILayout.PropertyField(_triggerProp, true);
    }
}
