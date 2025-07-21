using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class GameMenu : GameUI
{
    protected const float Delay = 0.8f;

    [SerializeField] private Button _closedButton;
    [SerializeField] private MenuButton _menuButton;
    [SerializeField] private Button _repeatButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _skipButton;
    [SerializeField] private Animator _animator;

    private Animator[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Animator>();
    }

    private void OnEnable()
    {
        _closedButton.onClick.AddListener(Close);
        _repeatButton.onClick.AddListener(ClickRepeatButton);
        _homeButton.onClick.AddListener(ClickHomeButton);
        _skipButton?.onClick.AddListener(ClickSkipLevel);
        YandexGame.RewardVideoEvent += Rewarder;
    }

    private void OnDisable()
    {
        _closedButton?.onClick.RemoveListener(Close);
        _repeatButton.onClick.RemoveListener(ClickRepeatButton);
        _homeButton.onClick.RemoveListener(ClickHomeButton);
        _skipButton?.onClick.RemoveListener(ClickSkipLevel);
        YandexGame.RewardVideoEvent -= Rewarder;
    }

    public override void Close()
    {
        base.Close();
        _animator.Play("Exit");
        Closed?.Invoke();
        _menuButton.Open();

        foreach (var button in _buttons)
        {
            button.Play("Exit");
        }
    }

    public override void Open()
    {
        base.Open();
        _animator.Play("Enter");

        foreach (var button in _buttons)
        {
            button.Play("Enter");
        }
    }

    private void Rewarder(int id)
    {
        OnSkipLevel?.Invoke();
        Close();
    }

    private void ClickSkipLevel()
    {
        YandexGame.RewVideoShow(0);
    }
}
