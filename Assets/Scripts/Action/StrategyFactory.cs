using System.Collections.Generic;

public abstract class StrategyFactory
{
    private EffectorFactory _factory;

    protected StrategyFactory(EffectorFactory factory)
    {
        _factory = factory;
    }

    protected List<Trigger> CreateTriggers(SwitchTriggerConfig config, Snake snake)
    {
        List<Trigger> createdTriggers = new List<Trigger>();

        foreach (var triggerConfig in config.Triggers)
        {
            Trigger trigger = (Trigger)_factory.Get(triggerConfig, snake);
            createdTriggers.Add(trigger);
        }

        return createdTriggers;
    }
}
