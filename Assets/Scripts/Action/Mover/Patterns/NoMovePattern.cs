using UnityEngine;

public class NoMovePattern : IMover
{
    private IMovable _movable;
    private float _rotationY;

    public NoMovePattern(IMovable movable, float rotationY)
    {
        _movable = movable;
        _rotationY = rotationY;
    }

    public void StartMove()
    {
        _movable.Transform.rotation = Quaternion.Euler(0, _rotationY, 0);
    }

    public void StopMove()
    {
        _movable.Transform.rotation = Quaternion.identity;
    }

    public void Update()
    {
        
    }
}
