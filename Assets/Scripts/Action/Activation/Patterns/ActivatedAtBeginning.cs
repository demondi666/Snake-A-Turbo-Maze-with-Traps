using UnityEngine.Events;

public class ActivatedAtBeginning : IActivated
{
    private IMovable _movable;

    public ActivatedAtBeginning(IMovable movable)
    {
        _movable = movable;
        _movable.Transform.gameObject.SetActive(false);
    }

    public void Activate()
    {
        _movable.Transform.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _movable.Transform.gameObject.SetActive(false);
    }
}
