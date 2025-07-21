using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/LevelConfig", fileName = "LevelConfig")]
public class LevelsConfig : ScriptableObject
{
    [NonReorderable]
    [SerializeField] private Level[] _levels = new Level[5];

    [field: SerializeField] public string LabelEng { get; private set; }
    [field: SerializeField] public string LabelRus { get; private set; }

    public Level[] Levels => _levels;
}
