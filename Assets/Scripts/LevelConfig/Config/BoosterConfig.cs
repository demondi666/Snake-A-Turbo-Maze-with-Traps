using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Effectors/BoosterConfig", fileName = "BoosterConfig")]
public class BoosterConfig : EffectorConfig
{
    [SerializeField] private Booster _prefab;

    private void OnValidate()
    {
        base.OnValidate();

        if (_prefab == null)
        {
            LogErrorAndStopGame();
        }
    }

    public override Effector GetPrefab()
    {
        return _prefab;
    }
}
