using System.Collections.Generic;

public class ActivatedByTriggerConfig : SwitchTriggerConfig, IActivatorConfig
{
    public ActivatedByTriggerConfig(List<TriggerConfig> triggers) : base(triggers)
    {
    }
}
