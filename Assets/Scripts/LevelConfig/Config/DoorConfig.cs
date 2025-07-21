using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Effectors/DoorConfig", fileName = "DoorConfig")]
public class DoorConfig : EffectorConfig
{
    [SerializeField] private Door _prefab;
    [SerializeField] public int _bonesToNextLevel;

    public int BonesToNextLevel => _bonesToNextLevel;

    private void OnValidate()
    {
        base.OnValidate();

        if (_prefab == null || _bonesToNextLevel<0)
        {
            LogErrorAndStopGame();
        }

    }

    public override Effector GetPrefab()
    {
        return _prefab;
    }
}
