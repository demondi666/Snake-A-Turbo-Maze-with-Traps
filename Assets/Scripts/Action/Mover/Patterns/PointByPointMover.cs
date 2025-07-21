using System.Collections.Generic;
using UnityEngine;

public class PointByPointMover : IMover
{
    private const float MinDistanceToTarget = 0.01f;

    private IMovable _movable;
    private Queue<Vector3> _targets;
    private float _speed;

    private Vector3 _currentTarget;
    private bool _isMoving;

    public PointByPointMover(IMovable movable, IEnumerable<Vector3> targets , float speed)
    {
        _movable = movable;
        _targets = new Queue<Vector3>(targets);
        _speed = speed;
        SwitchTarget();
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
        _movable.Transform.position = Vector3.MoveTowards(_movable.Transform.position, _currentTarget, _speed * Time.deltaTime);
        
        if (Vector3.Distance(_movable.Transform.position, _currentTarget) < MinDistanceToTarget)
            SwitchTarget();

        Rotate(_currentTarget);
    }

    private void SwitchTarget()
    {
        if (_currentTarget != Vector3.zero)
            _targets.Enqueue(_currentTarget);

        _currentTarget = _targets.Dequeue();
    }
    
    private void Rotate(Vector3 target)
    {
        Vector3 direction = target - _movable.Transform.position;
        _movable.Transform.rotation = Quaternion.LookRotation(direction);
    }
}
