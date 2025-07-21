using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MainMenu
{
    [SerializeField] private Button _levelButton;
    [SerializeField] private LevelMenu _levelMenu;

    private void OnEnable()
    {
        _levelButton.onClick.AddListener(OnClickLevelButton);
    }

    private void OnDisable()
    {
        _levelButton.onClick.RemoveListener(OnClickLevelButton);
    }

    private void OnClickLevelButton()
    {
        Close();
        _levelMenu.Open();
    }
}
