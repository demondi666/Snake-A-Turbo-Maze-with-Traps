using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(EffectorConfig), true)]
public class EffectorConfig_Editor : Editor
{
    private SerializedProperty _moverProp;
    private SerializedProperty _rotationYProp;
    private SerializedProperty _speedProp;
    private SerializedProperty _pointsProp;

    private SerializedProperty _activatedTypeProp;
    private SerializedProperty _timeActivationProp;
    private SerializedProperty _activatedTriggersProp;

    private SerializedProperty _deactivatedTypeProp;
    private SerializedProperty _timeDeactivationProp;
    private SerializedProperty _deactivatedTriggersProp;

    private SerializedProperty _spawnPointProp;

    protected void OnEnable()
    {
        _moverProp = serializedObject.FindProperty("_mover");
        _rotationYProp = serializedObject.FindProperty("_rotationY");
        _speedProp = serializedObject.FindProperty("_speed");
        _pointsProp = serializedObject.FindProperty("_points");

        _activatedTypeProp = serializedObject.FindProperty("_activatedType");
        _timeActivationProp = serializedObject.FindProperty("_timeActivation");
        _activatedTriggersProp = serializedObject.FindProperty("_activatedTriggers");

        _deactivatedTypeProp = serializedObject.FindProperty("_deactivatedType");
        _timeDeactivationProp = serializedObject.FindProperty("_timeDeactivation");
        _deactivatedTriggersProp = serializedObject.FindProperty("_deactivatedTriggers");

        _spawnPointProp = serializedObject.FindProperty("_spawnPoint");
    }

    public override void OnInspectorGUI()
    {
        EffectorConfig config = (EffectorConfig)target;

        EditorGUI.BeginChangeCheck();

        DrawMoverProperties(config);
        DrawActivatedTypeProperties(config);
        DrawDeactivatedTypeProperties(config);

        EditorGUILayout.PropertyField(_spawnPointProp, true);

        AdditionalInspectorGUI();

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawMoverProperties(EffectorConfig config)
    {
        EditorGUILayout.PropertyField(_moverProp);
        if (_moverProp.enumValueIndex != (int)config._mover)
        {
            Undo.RecordObject(target, "Changed Mover Type");
            config._mover = (MoverTypes)_moverProp.enumValueIndex;
        }

        switch ((MoverTypes)_moverProp.enumValueIndex)
        {
            case MoverTypes.NoMove:
                DrawFloatProperty("RotationY", _rotationYProp, ref config._rotationY);
                break;
            case MoverTypes.PointByPoint:
                DrawFloatProperty("Speed", _speedProp, ref config._speed);
                DrawListProperty("Points", _pointsProp);
                break;
            case MoverTypes.TargetFollower:
                DrawFloatProperty("Speed", _speedProp, ref config._speed);
                break;
        }
    }

    private void DrawActivatedTypeProperties(EffectorConfig config)
    {
        EditorGUILayout.PropertyField(_activatedTypeProp);
        if (_activatedTypeProp.enumValueIndex != (int)config._activatedType)
        {
            Undo.RecordObject(target, "Changed Activated Type");
            config._activatedType = (ActivatedTypes)_activatedTypeProp.enumValueIndex;
        }

        switch ((ActivatedTypes)_activatedTypeProp.enumValueIndex)
        {
            case ActivatedTypes.AfterWhile:
                DrawFloatProperty("Time Activation", _timeActivationProp, ref config._timeActivation);
                DrawListProperty("Activated Triggers", _activatedTriggersProp);
                break;
            case ActivatedTypes.Trigger:
                DrawListProperty("Activated Triggers", _activatedTriggersProp);
                break;
        }
    }

    private void DrawDeactivatedTypeProperties(EffectorConfig config)
    {
        EditorGUILayout.PropertyField(_deactivatedTypeProp);
        if (_deactivatedTypeProp.enumValueIndex != (int)config._deactivatedType)
        {
            Undo.RecordObject(target, "Changed Deactivated Type");
            config._deactivatedType = (ActivatedTypes)_deactivatedTypeProp.enumValueIndex;
        }

        switch ((ActivatedTypes)_deactivatedTypeProp.enumValueIndex)
        {
            case ActivatedTypes.AfterWhile:
                DrawFloatProperty("Time Deactivation", _timeDeactivationProp, ref config._timeActivation);
                DrawListProperty("Deactivated Triggers", _deactivatedTriggersProp);
                break;
            case ActivatedTypes.Trigger:
                DrawListProperty("Deactivated Triggers", _deactivatedTriggersProp);
                break;
        }
    }

    private void DrawFloatProperty(string label, SerializedProperty property, ref float value)
    {
        EditorGUILayout.PropertyField(property);
        if (property.floatValue != value)
        {
            Undo.RecordObject(target, $"Changed {label}");
            value = property.floatValue;
        }
    }

    private void DrawListProperty(string label, SerializedProperty property)
    {
        EditorGUILayout.PropertyField(property, true);
    }

    protected virtual void AdditionalInspectorGUI()
    {

    }
}
