using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameplayMediator : MonoBehaviour
{
    [SerializeField] private Bootstrap _bootstrap;
    [SerializeField] private float _activationTime;
    [SerializeField] private float _delayBeforeStart;
    [SerializeField] private CoroutineRunner _runner;
    [SerializeField] private MenuButton _menuButton;
    [SerializeField] private GameMenu _settingsMenu;
    [SerializeField] private TrainingUI _training;
    [SerializeField] private LoadingScreen _loadingScreen;
    [SerializeField] private LevelNumber _levelNumber;
    [SerializeField] private WinPanel _winPanel;
    [SerializeField] private CoinDisplay _coinDisplay;

    private Snake _snake;
    private Door _door;
    private IEnumerator _currentCoroutine;
    private Level _previousLevel;

    public UnityAction StartedNextLevel;
    public UnityAction LosingLevel;
    public UnityAction RepeatLevel;

    public void Initialize(Snake snake, Door door, Level level)
    {
        if (_snake != null)
            _snake.OnDied -= SnakeDied;

        if (_door != null)
            _door.LevelPassed -= OnLevelWin;

        _snake = snake;
        _door = door;

        _door.LevelPassed += OnLevelWin;
        _snake.OnDied += SnakeDied;
        _snake.OnPickUpCoin += PickedUpCoin;
        _previousLevel = level;
    }

    private void OnEnable()
    {
        _menuButton.MenuButtonClick += OnStopGame;
        _settingsMenu.Closed += OnPlay;
        _settingsMenu.Repeat += OnRepeatLevel;
        _settingsMenu.GoBackHome += OnGoBackHome;
        _settingsMenu.OnSkipLevel += StartNextLevel;
        _bootstrap.StartedLevel += OnStartedLevel;
        _bootstrap.CompletedLevels += OnCompletedLevels;
        _winPanel.Repeat += OnRepeatLevel;
        _winPanel.GoBackHome += OnGoBackHome;
        _winPanel.OnNextLevel += StartNextLevel;
    }

    private void OnDisable()
    {
        _menuButton.MenuButtonClick -= OnStopGame;

        if (_snake != null)
        {
            _snake.OnDied -= SnakeDied;
            _snake.OnPickUpCoin -= PickedUpCoin;
        }

        _door.LevelPassed -= OnLevelWin;
        _settingsMenu.Closed -= OnPlay;
        _settingsMenu.Repeat -= OnRepeatLevel;
        _settingsMenu.GoBackHome -= OnGoBackHome;
        _settingsMenu.OnSkipLevel -= StartNextLevel;
        _bootstrap.StartedLevel -= OnStartedLevel;
        _bootstrap.CompletedLevels -= OnCompletedLevels;
        _winPanel.Repeat -= OnRepeatLevel;
        _winPanel.GoBackHome -= OnGoBackHome;
        _winPanel.OnNextLevel -= StartNextLevel;
    }

    private void SnakeDied()
    {
        _currentCoroutine = CheckForKeyPress(LosingLevel);
        StartCoroutine(_currentCoroutine);
    }

    private void OnLevelWin()
    {
        _previousLevel.CoinsChange(_snake.GetCoins());
        _winPanel.Open();
        OnStopGame();
    }

    private IEnumerator CheckForKeyPress(UnityAction action)
    {
        _runner.DeactivatedCoroutines();

        yield return new WaitForSeconds(_delayBeforeStart);

        while (true)
        {
            if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            {
                OnPlay();
                action?.Invoke();
                StopCoroutine(_currentCoroutine);
                yield break;
            }

            yield return null;
        }
    }

    private void OnRepeatLevel()
    {
        RepeatLevel?.Invoke();
        _runner.DeactivatedCoroutines();
        OnPlay();
    }

    private void OnStopGame()
    {
        Time.timeScale = 0;
        _training.Close();
        _levelNumber.Close();
    }

    private void OnPlay()
    {
        Time.timeScale = 1;
        _levelNumber.Open();
        _training.Open();
    }

    private void OnGoBackHome()
    {
        OnPlay();
        _loadingScreen.LoadMenu();
    }

    private void StartNextLevel()
    {
        StartedNextLevel?.Invoke();
        _winPanel.Close();
        OnPlay();
    }

    private void OnStartedLevel(Level level)
    {
        if (level.IsBeginnerLevel)
        {
            _training.AnimateFade();
        }

        _levelNumber.ChangeLevelNumber(level.LevelNumber);
    }

    private void OnCompletedLevels()
    {
        _winPanel.Open();
        OnStopGame();
    }

    private void PickedUpCoin()
    {
        _coinDisplay.CoinsChange();
    }
}
