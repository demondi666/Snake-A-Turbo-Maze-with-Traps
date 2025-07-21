using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MainMenu
{
    [SerializeField] private GameObject _itemContainer;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private StartMenu _startMenu;
    [SerializeField] private LevelContainer _levelContainer;
    [SerializeField] private LoadingScreen _loadingScreen;

    private Level _previousLevel;
    private List<LevelsConfig> _configs;
    private LevelView[] _views;
    
    private void Start()
    {
        _levelContainer.UploadData();
        _configs = _levelContainer.Configs;
        _views = _itemContainer.gameObject.GetComponentsInChildren<LevelView>();

        RendererLevels();
    }

    private void OnEnable()
    {
        _backToMenuButton.onClick.AddListener(OnBackToMenu);
    }

    private void OnDisable()
    {
        _backToMenuButton.onClick.RemoveListener(OnBackToMenu);
    }

    private void AddLevel(LevelsConfig config, LevelView view, bool isLastOpenLevel)
    {
        view.Render(config,isLastOpenLevel);
        view.StartLevel += OnStartLevelClick;
    }

    private void RendererLevels()
    {
        for (int i = 0; i < _configs.Count; i++)
        {
            int countLevels = _configs[i].Levels.Length;

            bool isFirstOpen = _configs[i].Levels[0].IsLevelOpen;
            bool isLastClosed = !_configs[i].Levels[countLevels - 1].IsLevelOpen;
            bool hasNextConfig = (i + 1) < _configs.Count;
            bool isNextFirstOpen = hasNextConfig && _configs[i + 1].Levels[0].IsLevelOpen;

            bool specialAdd = isFirstOpen && (isLastClosed || !isNextFirstOpen);

            AddLevel(_configs[i], _views[i], specialAdd);
        }
    }

    public override void Open()
    {
        base.Open();

        foreach (var view in _views)
        {
            view.Open(); 
        }
    }

    private void OnStartLevelClick(LevelsConfig level, LevelView view)
    {
        if (level.Equals(_configs[0]))
        {
            TryStartLevel(level, view);
            return;
        }

        for (int i = 1; i < _configs.Count; i++)
        {
            if (level.Equals(_configs[i]))
            {
                _previousLevel = _configs[i].Levels[0];
                TryStartLevel(level, view);
                return;
            }
        }
    }
    
    private void TryStartLevel(LevelsConfig config, LevelView view)
    {
        if (config.Equals(_configs[0]) || _previousLevel.IsLevelOpen == true)
        {
            view.StartLevel -= OnStartLevelClick;

            _loadingScreen.LoadLevel(config.Levels[0]);
        }
    }

    private void OnBackToMenu()
    {
        foreach (var view in _views)
        {
            view.Close();
        }

        Close();
        _startMenu.Open();
    }
}
