using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trap : Effector
{
    [SerializeField] protected AudioClip Appearance;

    protected Tweener UpperTweener;

    [SerializeField] private float _targetHeight;
    [SerializeField] private float _duration;
    [SerializeField] private AudioClip _hit;

    private Vector3 _originalPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Snake>(out Snake snake))
        {
            snake.Die();
            PlaySound();
        }
        else if(other.TryGetComponent<Bone>(out Bone bone))
        {
            bone.Demolish();
            PlaySound();
        }
    }

    protected void SavePosition()
    {
        _originalPosition = Model.transform.localPosition;

        Model.transform.localPosition = Vector3.down;
        Appear();
        UpperTweener.Restart();
        MakeAppearanceSound();
    }

    protected void ResetPosition()
    {
        if (UpperTweener != null)
        {
            UpperTweener.Kill();
            Model.transform.localPosition = _originalPosition;
        }
    }

    protected void MakeAppearanceSound()
    {
        AudioSource.PlayOneShot(Appearance);
    }

    private void Appear()
    {
        UpperTweener = Model?.transform.DOMoveY(_targetHeight, _duration)
                 .SetEase(Ease.InOutSine);
    }

    private void PlaySound()
    {
        if(_hit!=null)
            AudioSource.PlayOneShot(_hit);
    }
}
