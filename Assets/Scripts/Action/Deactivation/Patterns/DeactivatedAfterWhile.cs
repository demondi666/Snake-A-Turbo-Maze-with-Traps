using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatedAfterWhile : IDeactivated
{
    private float _deactivationTime;
    private IMovable _movable;
    private List<Trigger> _triggers;

    private Coroutine _deactivationCoroutine;

    public DeactivatedAfterWhile(float deactivationTime, IMovable movable, List<Trigger> triggers)
    {
        _triggers = triggers;
        _deactivationTime = deactivationTime;
        _movable = movable;
    }

    public void Deactivate()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Activated += CountdownToDeactivation;
        }
    }

    private void CountdownToDeactivation()
    {
        if (_deactivationCoroutine == null)
        {
            _deactivationCoroutine = CoroutineRunner.Instance.ActivateCoroutine(DeactivateAfterDelay(_deactivationTime));
        }

        foreach (var trigger in _triggers)
        {
            trigger.Activated -= CountdownToDeactivation;
        }
    }

    private IEnumerator DeactivateAfterDelay(float deactivationTime)
    {
        yield return new WaitForSeconds(deactivationTime);
        _movable.Transform.gameObject.SetActive(false);
        _deactivationCoroutine = null;
    }
}
