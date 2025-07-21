using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : Trigger
{
    [SerializeField] private float _targetHeight = 0f;
    [SerializeField] private float _duration = 1f;

    private Tweener _lowerTweener;
    private Vector3 _originalPosition;

    private void OnEnable()
    {
        _originalPosition = Model.transform.localPosition;

        if (_lowerTweener != null)
            _lowerTweener.Kill();
    }

    private void OnDisable()
    {
        if (_lowerTweener != null)
        {
            _lowerTweener.Kill();
            Model.transform.localPosition = _originalPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if(other.TryGetComponent<Snake>(out Snake snake) || other.TryGetComponent<Bone>(out Bone bone))
        {
            Animate();
        }
    }

    protected override void Animate()
    {
        _lowerTweener =  Model.transform.DOMoveY(_targetHeight, _duration)
                 .SetEase(Ease.InOutSine);
    }
}
