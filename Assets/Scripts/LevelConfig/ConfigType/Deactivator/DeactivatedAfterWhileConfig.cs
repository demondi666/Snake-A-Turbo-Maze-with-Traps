using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivatedAfterWhileConfig : SwitchTriggerConfig,  IDeactivatorConfig
{
    private float _timeDeactivate;

    public DeactivatedAfterWhileConfig(List<TriggerConfig> triggers, float timeDeactivate) : base(triggers)
    {
        _timeDeactivate = timeDeactivate;
    }

    public float TimeDeactivate => _timeDeactivate;
}
