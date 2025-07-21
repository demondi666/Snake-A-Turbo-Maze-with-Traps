using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using YG;
using System.Linq;

public class LevelView : View
{
    [SerializeField] private Button _startButton;
    [SerializeField] private GameObject _lockImage;
    [SerializeField] private GameObject _openImage;
    [NonReorderable]
    [SerializeField] private GameObject[] _stars = new GameObject[3];
    
    private LevelsConfig _config;
    private Animator _animator;

    public UnityAction<LevelsConfig, LevelView> StartLevel;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _startButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnButtonClick);
        YandexGame.SwitchLangEvent -= ChangeLanguage;
    }

    public void Render(LevelsConfig level, bool isLastOpenLevel)
    {
        _config = level;
        YandexGame.SwitchLangEvent += ChangeLanguage;
        ChangeLanguage(YandexGame.savesData.language);
        ChangeBackground(isLastOpenLevel);
        ChangeStars();
    }
    
    public override void OnButtonClick()
    {
        StartLevel?.Invoke(_config, this);
    }

    public void Open()
    {
        _animator.Play("Enter");
    }
    
    public void Close()
    {
        _animator.Play("Exit");
    }

    private void ChangeLanguage(string language)
    {
        switch (language)
        {
            case "ru":
                Label.text = _config.LabelRus;
                break;
            case "en":
                Label.text = _config.LabelEng;
                break;
        }
    }

    private void ChangeBackground(bool isLastOpenLevel)
    {
        if(_config.Levels[0].IsLevelOpen == true)
        {
            _lockImage.gameObject.SetActive(false);
            _startButton.interactable = true;
        }
        else
        {
            _lockImage.gameObject.SetActive(true);
            _startButton.interactable = false;
            _openImage.gameObject.SetActive(false);
        }

        if (isLastOpenLevel == true)
        {
            _openImage.gameObject.SetActive(true);
        }
    }

    private void ChangeStars()
    {
        int sumStars = _config.Levels.Sum(level => level.SelectedCoins);
        float averageStars = (float)sumStars / _config.Levels.Length;

        int filledStars = Mathf.RoundToInt(averageStars);

        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].SetActive(averageStars >= i + 1);
        }
    }
}
