using System.Collections.Generic;

public class DeactivateByTriggerConfig : SwitchTriggerConfig, IDeactivatorConfig
{
    public DeactivateByTriggerConfig(List<TriggerConfig> triggers) : base(triggers)
    {
    }
}
