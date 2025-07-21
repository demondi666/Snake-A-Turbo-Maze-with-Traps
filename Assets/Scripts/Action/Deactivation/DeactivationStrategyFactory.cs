using System;

public class DeactivationStrategyFactory:StrategyFactory
{
    public DeactivationStrategyFactory(EffectorFactory factory) : base(factory)
    {
    }

    public IDeactivated Get(IMovable mover, IDeactivatorConfig config, Snake snake)
    {
        switch (config)
        {
            case DeactivateByTriggerConfig:
                return new DeactivatedByTrigger(CreateTriggers((DeactivateByTriggerConfig)(config), snake), mover);
            case DeactivatedAfterWhileConfig:
                return new DeactivatedAfterWhile(((DeactivatedAfterWhileConfig)config).TimeDeactivate, mover, CreateTriggers((DeactivatedAfterWhileConfig)config, snake));
            case DeactivateAtEndConfig:
                return new DeactivatedAtEnd();
            default: 
                throw new ArgumentException(nameof(config));
        }
    }
}
