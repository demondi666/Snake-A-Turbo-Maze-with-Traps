using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Booster : Effector
{
    [SerializeField] private float _animationDuration = 1f;
    [SerializeField] private float _liftHeight = 1f;
    [SerializeField, Range(1f, 10f)] private float _magnificationFactor;
    [SerializeField] private Vector3 _targetScale;

    private Vector3 _originalScale;
    private float _originalYPosition;
    private Tweener _scaleTweener;
    private Tweener _moveTweener;
    private Tweener _rotateTweener;
    private Vector3 _rotationAmount = new Vector3(0, 360f, 0);
   
    private void Start()
    {
        _originalScale = Model.transform.localScale;
        _originalYPosition = Model.transform.position.y;
        _targetScale = _originalScale * _magnificationFactor;
    }

    private void OnEnable()
    {
        Animate();
        _scaleTweener.Play();
        _moveTweener.Play();
        _rotateTweener.Play();
    }

    private void OnDisable()
    {
        _scaleTweener.Pause();
        _moveTweener.Pause();
        _rotateTweener.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Snake>(out Snake snake))
        {
            EventHappened(snake);
        }
    }

    protected override void Animate()
    {
        _scaleTweener = Model.transform.DOScale(_targetScale, _animationDuration)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);

        _moveTweener = Model.transform.DOMoveY(_originalYPosition + _liftHeight, _animationDuration)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);

        _rotateTweener = Model.transform.DORotate(_rotationAmount, _animationDuration, RotateMode.LocalAxisAdd)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
    }

    protected abstract void EventHappened(Snake snake);
}
