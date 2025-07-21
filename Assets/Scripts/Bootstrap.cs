using IJunior.TypedScenes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

public class Bootstrap : MonoBehaviour, ISceneLoadHandler<Level>
{
    [SerializeField] private GameplayMediator _mediator;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Level _previousLevel;
    [SerializeField] private LoadingScreen _loadingScreen;

    private LevelContainer _levelContainer;

    public UnityAction<Level> StartedLevel;
    public UnityAction CompletedLevels;

    public void OnSceneLoaded(Level argument)
    {
        _previousLevel = argument;
    }

    private void Awake()
    {
        _levelContainer = FindObjectOfType<LevelContainer>();
    }

    private void Start()
    {
        StartLevel(_previousLevel);
    }

    private void OnEnable()
    {
        _mediator.LosingLevel += OnRestartLevel;
        _mediator.StartedNextLevel += OnStartNextLevel;
        _mediator.RepeatLevel += OnRestartLevel;
    }

    private void OnDisable()
    {
        _mediator.LosingLevel -= OnRestartLevel;
        _mediator.StartedNextLevel -= OnStartNextLevel;
        _mediator.RepeatLevel -= OnRestartLevel;
    }

    private void StartLevel(Level level)
    {
        _spawner.Spawn(_mediator, level);
        StartedLevel?.Invoke(level);
    }

    private void OnRestartLevel()
    {
        Transition();
    }

    private void OnStartNextLevel()
    {
        for (int i = 0; i < _levelContainer.GetLevels().Count; i++)
        {
            if (_levelContainer.GetLevels()[i] == _previousLevel && (i+1)< _levelContainer.GetLevels().Count)
            {
                _previousLevel = _levelContainer.GetLevels()[i + 1];
                _previousLevel.LevelPassed();
                YandexGame.savesData.SaveLevels(_levelContainer.GetLevels());
                _loadingScreen.LoadLevel(_previousLevel);
                break;
                
            }
            else if (_levelContainer.Configs[i] == _previousLevel && (i + 1) >= _levelContainer.Configs.Count)
            {
                CompletedLevels?.Invoke();
            }
        }
    }

    private void Transition()
    {
        Game.LoadAsync(_previousLevel);
    }
}
