using System.Collections.Generic;
using UnityEngine;
using YG;

public class LevelContainer : MonoBehaviour
{
    [SerializeField] private List<LevelsConfig> _configs;

    private List<Level> _levels = new List<Level>();

    public List<LevelsConfig> Configs => _configs;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_levels.Count == 0)
        {
            foreach (var config in Configs)
            {
                foreach (var level in config.Levels)
                {
                    _levels.Add(level);
                }
            }
        }
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadLevels;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadLevels;
    }

    public List<Level> GetLevels()
    {
        return _levels;
    }

    public void UploadData()
    {
       if (YandexGame.SDKEnabled == true)
        {
            YandexGame.savesData.Init(_levels.Count);
            YandexGame.savesData.SaveLevels(_levels);
            DontDestroyOnLoad(gameObject);
            LoadLevels();
        }
    }

    private void LoadLevels()
    {
        if (YandexGame.savesData.OpenLevels.Length > 0)
        {
            for (int i = 0; i < _levels.Count; i++)
            {
                if (YandexGame.savesData.OpenLevels != null && YandexGame.savesData.OpenLevels[i] == true)
                {
                    _levels[i].LevelPassed();
                }
                else
                {
                    break;
                }
            }
        }
    }
    
}
