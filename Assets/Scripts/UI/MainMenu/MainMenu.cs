using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MainMenu : UI
{
    protected const float Delay = 0.8f;

    private Animator[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<Animator>();
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;

        foreach (var button in _buttons)
        {
            button.Play("Enter");
        }
    }

    public override void Close()
    {
        foreach (var button in _buttons)
        {
            button.Play("Exit");
        }

        StartCoroutine(DelayTransition());
    }

    protected virtual IEnumerator DelayTransition()
    {
        yield return new WaitForSeconds(Delay);
        CloseMenu();
    }

    private void CloseMenu()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
    }
}
