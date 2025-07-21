using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class TrainingUI : UI
{
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private int _numberOfLoops = 5;
    [SerializeField] private float _appendInterval = 1f;
    private Sequence _fadeSequence;

    private void Start()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            gameObject.SetActive(false);
        }    
    }

    public void AnimateFade()
    {
        _fadeSequence = DOTween.Sequence();

        _fadeSequence.Append(CanvasGroup.DOFade(1f, _fadeDuration));
        _fadeSequence.Append(CanvasGroup.DOFade(0f, _fadeDuration));

        _fadeSequence.SetLoops(_numberOfLoops, LoopType.Restart);
        _fadeSequence.AppendInterval(_appendInterval);
    }

    private void OnDestroy()
    {
        _fadeSequence.Kill();
    }

    public override void Open()
    {
        _fadeSequence.Play();
    }

    public override void Close()
    {
        _fadeSequence.Pause();
        CanvasGroup.alpha = 0;
    }
}
