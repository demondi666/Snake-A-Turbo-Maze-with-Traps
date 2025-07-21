using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetConfig : MovableConfig
{
    private float _speed;

    public float Speed => _speed;

    public MoveToTargetConfig(float speed)
    {
        _speed = speed;
    }
}
