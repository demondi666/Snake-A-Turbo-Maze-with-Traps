using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedAfterWhileConfig : SwitchTriggerConfig,  IActivatorConfig
{
    private float _timeActivation;

    public ActivatedAfterWhileConfig(List<TriggerConfig> triggers, float timeActivation) : base(triggers)
    {
        _timeActivation = timeActivation;
    }

    public float TimeActivation => _timeActivation;
}
