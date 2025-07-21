using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class GameUI : UI
{
    public UnityAction Closed;
    public UnityAction Repeat;
    public UnityAction GoBackHome;
    public UnityAction OnSkipLevel;
    public UnityAction OnNextLevel;

    public override void Close()
    {
        CanvasGroup.alpha = 0;
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;
    }

    protected virtual void ClickRepeatButton()
    {
        Repeat?.Invoke();
        Close();
    }

    protected void ClickHomeButton()
    {
        GoBackHome?.Invoke();
    }
}
