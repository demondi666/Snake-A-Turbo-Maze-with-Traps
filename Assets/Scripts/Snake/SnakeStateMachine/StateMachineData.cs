using System;
using UnityEngine;

public class StateMachineData
{
    private float _speed;
    private float _xInput;
    private float _speedRotation;
    private float _bonesDistance;
    private float _smoothTime;

    public float Speed
    {
        get => _speed;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(_speed));
            _speed = value;
        }
    }

    public float XInput
    {
        get => _xInput;
        set
        {
            if (_xInput < -1 || _xInput > 1)
            {
                throw new ArgumentOutOfRangeException(nameof(_xInput));
            }
            _xInput = value;
        }
    }

    public float SpeedRotation
    {
        get => _speedRotation;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(_speed));
            _speedRotation = value;
        }
    }

    public float BonesDistance
    {
        get => _bonesDistance;
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(_speed));
            _bonesDistance = value;
        }
    }

    public float SmoothTime
    {
        get => _smoothTime;
        set
        {
            if(value<0)
                throw new ArgumentOutOfRangeException(nameof(_smoothTime));
            _smoothTime = value;
        }
    }
}
