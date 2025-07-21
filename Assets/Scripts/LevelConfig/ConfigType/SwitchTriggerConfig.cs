using System.Collections.Generic;

public abstract class SwitchTriggerConfig 
{
    private List<TriggerConfig> _triggers;

    public SwitchTriggerConfig(List<TriggerConfig> triggers)
    {
        _triggers = triggers;
    }

    public List<TriggerConfig> Triggers => _triggers;
}
