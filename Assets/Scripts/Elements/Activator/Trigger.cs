using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Trigger : Effector
{
    public UnityAction Activated;
    protected void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Snake>(out Snake snake) || other.TryGetComponent<Bone>(out Bone bone))
        {
            Activated?.Invoke();
        }
    }
}
