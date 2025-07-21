using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedAfterWhile : IActivated
{
    private float _activationTime;
    private IMovable _movable;
    private List<Trigger> _triggers;

    private Coroutine _activationCoroutine;

    public ActivatedAfterWhile(float timeBeforeActivation, IMovable movable, List<Trigger> triggers)
    {
        _activationTime = timeBeforeActivation;
        _movable = movable;
        _triggers = triggers;

        _movable.Transform.gameObject.SetActive(false);
    }

    public void Activate()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Activated += CountdownToActivation;
        }
    }

    private void CountdownToActivation()
    {
        if (_activationCoroutine == null)
        {
            _activationCoroutine = CoroutineRunner.Instance.ActivateCoroutine(ActivateAfterDelay(_activationTime));
        }

        foreach (var trigger in _triggers)
        {
            trigger.Activated -= CountdownToActivation;
        }
    }

    private IEnumerator ActivateAfterDelay(float activationTime)
    {
        yield return new WaitForSeconds(activationTime);
        _movable.Transform.gameObject.SetActive(true);
        _activationCoroutine = null;
    }

    public void Deactivate()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Activated -= CountdownToActivation;
        }

        _movable.Transform.gameObject.SetActive(false);
    }
}
