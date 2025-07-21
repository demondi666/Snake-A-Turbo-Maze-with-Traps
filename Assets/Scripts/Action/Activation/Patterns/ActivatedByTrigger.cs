using System.Collections.Generic;

public class ActivatedByTrigger : IActivated
{
    private List<Trigger> _triggers;
    private IMovable _movable;

    public ActivatedByTrigger(List<Trigger> triggers, IMovable movable)
    {
        _triggers = new List<Trigger>(triggers);
        _movable = movable;
        _movable.Transform.gameObject.SetActive(false);
    }

    public void Activate()
    {
        foreach (var trigger in _triggers)
        {
            trigger.Activated += OnActivate;
        }
    }

    public void Deactivate()
    {
        _movable.Transform.gameObject.SetActive(false);

        foreach (var trigger in _triggers)
        {
            trigger.Activated -= OnActivate;
        }
    }

    private void OnActivate()
    {
        _movable.Transform.gameObject.SetActive(true);

        foreach (var trigger in _triggers)
        {
            trigger.Activated -= OnActivate;
        }
    }
}
