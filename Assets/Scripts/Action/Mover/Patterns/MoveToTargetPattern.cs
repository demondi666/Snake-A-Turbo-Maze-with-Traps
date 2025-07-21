using UnityEngine;

public class MoveToTargetPattern : IMover
{
    private IMovable _movable;
    private Transform _target;
    private float _speed;

    private bool _isMoving;

    public MoveToTargetPattern(IMovable movable, Transform target , float speed)
    {
        _movable = movable;
        _target = target;
        _speed = speed;
    }

    public void StartMove()
    {
        _isMoving = true;
    }

    public void StopMove()
    {
        _isMoving = false;
    }

    public void Update()
    {
        if (_isMoving == false)
        {
            return;
        }

        _movable.Transform.position = Vector3.MoveTowards(_movable.Transform.position, _target.position, _speed * Time.deltaTime);

        Rotate(_target.position);
    }

    private void Rotate(Vector3 target)
    {
        Vector3 direction = (target - _movable.Transform.position).normalized;
        Vector3 flatDirection = new Vector3(direction.x, 0, direction.z).normalized;

        if(flatDirection != Vector3.zero)
            _movable.Transform.rotation = Quaternion.LookRotation(flatDirection, Vector3.up);
    }
}
