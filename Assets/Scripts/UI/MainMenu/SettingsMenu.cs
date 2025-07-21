using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsMenu : MainMenu
{
    [SerializeField] private MenuButton _menuButton;
    [SerializeField] private Button _closedButton;

    public UnityAction ClosedSettings;

    private void OnEnable()
    {
        _closedButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _closedButton.onClick.RemoveListener(Close);
    }

    protected override IEnumerator DelayTransition()
    {
        ClosedSettings?.Invoke();
        _menuButton.Open();

        yield return base.DelayTransition();
    }
}
