using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/CharacterConfig", fileName = "CharacterConfig")]
public class CharacterConfig:ScriptableObject
{
    [field:SerializeField, Range(0, 10)] public float Speed { get; private set; }
    [field:SerializeField, Range(0, 100)] public float SpeedRotation { get; private set; }
    [field:SerializeField, Range(0,4)] public float BonesDistance { get; private set; }
    [field: SerializeField, Range(0, 1)] public float SmoothTimeBones { get; private set; }
}
