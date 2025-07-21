using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointByPointConfig: MovableConfig
{
    private float _speed;
    private List<Vector3> _points;

    public float Speed => _speed;
    public List<Vector3> Points => _points;

    public PointByPointConfig(float speed, List<Vector3> points)
    {
        _speed = speed;
        _points = points;
    }
}
