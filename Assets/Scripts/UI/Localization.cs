using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Localization : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private string _russionLanguage;
    [SerializeField] private string _englishLanguage;
    [SerializeField] private Sprite _russionSprite;
    [SerializeField] private Sprite _englishSprite;

    private string _previosLanguage;

    private void Awake()
    {
        _previosLanguage = YandexGame.savesData.language;
        ChangeLanguage(_previosLanguage);
    }

    private void OnEnable()
    {
        YandexGame.SwitchLangEvent += ChangeLanguage;
        _button.onClick.AddListener(SwitchLanguage);
    }

    private void OnDisable()
    {
        YandexGame.SwitchLangEvent -= ChangeLanguage;
        _button.onClick.RemoveListener(SwitchLanguage);
    }

    private void ChangeLanguage(string language)
    {
        switch (language)
        {
            case "ru":
                _button.image.sprite = _russionSprite;
                break;
            case "en":
                _button.image.sprite = _englishSprite;
                break;
        }

        _previosLanguage = language;
    }

    private void SwitchLanguage()
    {
        if (_previosLanguage.Equals(_russionLanguage))
        {
            YandexGame.SwitchLanguage(_englishLanguage);
            _previosLanguage = _englishLanguage;
            ChangeLanguage(_previosLanguage);
        }
        else if(_previosLanguage.Equals(_englishLanguage))
        {
            YandexGame.SwitchLanguage(_russionLanguage);
            _previosLanguage = _russionLanguage;
            ChangeLanguage(_previosLanguage);
        }
        else
        {
            throw new InvalidCastException(nameof(_previosLanguage));
        }
    }
}
