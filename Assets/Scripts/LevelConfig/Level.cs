using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Level", fileName = "Level")]
public class Level : ScriptableObject
{
    [field: SerializeField] public bool IsLevelOpen { get; private set; }
    [field: SerializeField] public bool IsBeginnerLevel { get; private set; }
    [field: SerializeField, Range(0, 4)] public int LevelNumber { get; private set; }


    [SerializeField] private List<EffectorConfig> _effectorConfigs;
    [SerializeField] private SnakeConfig _snakeConfig;

    private int _selectedCoins;

    public List<EffectorConfig> EffectorConfigs => _effectorConfigs;
    public SnakeConfig SnakeConfig => _snakeConfig;
    public int SelectedCoins => _selectedCoins;

    private void OnValidate()
    {
        if (_effectorConfigs.Any(config => config is DoorConfig)==false || _effectorConfigs.Any(config =>config==null) || _snakeConfig==null)
        {
            LogErrorAndStopGame();
        }
    }

    private void LogErrorAndStopGame()
    {
        string errorMessage = "_requiredObject is not assigned or is assigned incorrectly!";
        Debug.LogError(errorMessage, this);
    }

    public List<EffectorConfig> GetEffectors()
    {
        List<EffectorConfig> effectors = new List<EffectorConfig>();
        effectors.AddRange(_effectorConfigs);
        return effectors;
    }
    public void LevelPassed()
    {
        IsLevelOpen = true;
    }

    public void CoinsChange(int coinNumber)
    {
        if (coinNumber >_selectedCoins && (coinNumber <= 3 || coinNumber >= 0) )
            _selectedCoins = coinNumber;
    }
}
