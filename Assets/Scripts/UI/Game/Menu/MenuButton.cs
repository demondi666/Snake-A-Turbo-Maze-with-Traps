using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButton : UI
{
    [SerializeField] private GameMenu _settingsMenu;

    private Button _menuButton;

    public UnityAction MenuButtonClick;

    private void Awake()
    {
        _menuButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _menuButton.onClick.AddListener(MenuClick);
    }

    private void OnDisable()
    {
        _menuButton.onClick.RemoveListener(MenuClick);
    }

    private void MenuClick()
    {
        MenuButtonClick?.Invoke();
        Close();
        _settingsMenu.Open();
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
    }
}
