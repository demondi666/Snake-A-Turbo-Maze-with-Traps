using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EffectorFactory
{
    private MovementStrategyFactory _movementFactory;
    private ActivationStrategyFactory _activationFactory;
    private DeactivationStrategyFactory _deactivationFactory;

    private DiContainer _container;

    public EffectorFactory(DiContainer container)
    {
        _container = container;
        _movementFactory = new MovementStrategyFactory();
        _activationFactory = new ActivationStrategyFactory(this);
        _deactivationFactory = new DeactivationStrategyFactory(this);
    }

    public Effector Get(EffectorConfig config, Snake snake)
    {
        Effector effector = _container.InstantiatePrefab(config.GetPrefab(), config.SpawnPoint, Quaternion.identity, null).GetComponent<Effector>();
        _container.Inject(effector);

        effector.SetActive(_activationFactory.Get(effector, config.GetActivator(), snake));
        effector.SetMover(_movementFactory.Get(effector, config.GetMovable(), snake));
        effector.SetDeactivator(_deactivationFactory.Get(effector, config.GetDeactivator(), snake));
        
        return effector;
    }
}
