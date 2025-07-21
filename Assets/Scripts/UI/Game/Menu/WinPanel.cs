using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel : GameUI
{
    [SerializeField] private Button _repeatButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _nextLevelButton;

    private void OnEnable()
    {
        _repeatButton.onClick.AddListener(ClickRepeatButton);
        _homeButton.onClick.AddListener(ClickHomeButton);
        _nextLevelButton.onClick.AddListener(ClickNextLevel);
    }

    private void OnDisable()
    {
        _repeatButton.onClick.RemoveListener(ClickRepeatButton);
        _homeButton.onClick.RemoveListener(ClickHomeButton);
        _nextLevelButton.onClick.RemoveListener(ClickNextLevel);
    }

    private void ClickNextLevel()
    {
        OnNextLevel?.Invoke();
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Open()
    {
        base.Open();
    }
}
