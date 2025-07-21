using System;

public class ActivationStrategyFactory: StrategyFactory
{
    public ActivationStrategyFactory(EffectorFactory factory) : base(factory)
    {
    }

    public IActivated Get(IMovable mover, IActivatorConfig config, Snake snake)
    {
        switch (config)
        {
            case ActivatedByTriggerConfig:
                return new ActivatedByTrigger(CreateTriggers((ActivatedByTriggerConfig)config, snake), mover);
            case ActivatedAtBeginningConfig:
                return new ActivatedAtBeginning(mover);
            case ActivatedAfterWhileConfig:
                return new ActivatedAfterWhile(((ActivatedAfterWhileConfig)config).TimeActivation, mover, CreateTriggers((ActivatedAfterWhileConfig)config, snake));
            default:
                throw new ArgumentException(nameof(config));
        }
    }
}
