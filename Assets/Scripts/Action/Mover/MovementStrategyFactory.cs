using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementStrategyFactory
{
    public IMover Get(IMovable movable, MovableConfig config, Snake snake)
    {
        switch (config)
        {
            case NoMoveConfig:
                return new NoMovePattern(movable, ((NoMoveConfig)config).RotationY);
            case PointByPointConfig:
                return new PointByPointMover(movable, ((PointByPointConfig)config).Points,
                    ((PointByPointConfig)config).Speed);
            case MoveToTargetConfig:
                return new MoveToTargetPattern(movable, snake.transform,
                    ((MoveToTargetConfig)config).Speed);
            default:
                throw new ArgumentException(nameof(config));
        }
    }
}
