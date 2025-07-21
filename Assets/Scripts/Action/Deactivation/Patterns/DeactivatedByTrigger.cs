using System.Collections.Generic;

public class DeactivatedByTrigger : IDeactivated
{
    private List<Trigger> _triggers;
    private IMovable _movable;

    public DeactivatedByTrigger(List<Trigger> triggers, IMovable movable)
    {
        _triggers = triggers;
        _movable = movable;
    }

    public void Deactivate()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Activated += OnDeactivate;
        }
    }

    private void OnDeactivate()
    {
        _movable.Transform.gameObject.SetActive(false);

        foreach (var trigger in _triggers)
        {
            trigger.Activated -= OnDeactivate;
        }
    }
}
