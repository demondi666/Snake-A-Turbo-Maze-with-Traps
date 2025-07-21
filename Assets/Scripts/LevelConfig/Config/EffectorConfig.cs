using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class EffectorConfig : ScriptableObject
{
    [SerializeField] public MoverTypes _mover;
    [SerializeField] public float _rotationY;
    [SerializeField] public float _speed;
    [SerializeField] public List<Vector3> _points;

    [SerializeField] public ActivatedTypes _activatedType;
    [SerializeField] public float _timeActivation;
    [SerializeField] public List<TriggerConfig> _activatedTriggers;

    [SerializeField] public ActivatedTypes _deactivatedType;
    [SerializeField] public float _timeDeactivation;
    [SerializeField] public List<TriggerConfig> _deactivatedTriggers;

    [SerializeField] public Vector3 _spawnPoint;

    public Vector3 SpawnPoint => _spawnPoint;

    protected void OnValidate()
    {
        CheckParameters();
    }

    public abstract Effector GetPrefab();
    public MovableConfig GetMovable()
    {
        switch (_mover)
        {
            case MoverTypes.NoMove:
                return new NoMoveConfig(_rotationY);
            case MoverTypes.PointByPoint:
                return new PointByPointConfig(_speed, _points);
            case MoverTypes.TargetFollower:
                return new MoveToTargetConfig(_speed);
            default:
                throw new ArgumentException(nameof(_mover));
        }
    }

    public IActivatorConfig GetActivator()
    {
        switch (_activatedType)
        {
            case ActivatedTypes.AfterWhile:
                return new ActivatedAfterWhileConfig(_activatedTriggers, _timeActivation);
            case ActivatedTypes.Beginning:
                return new ActivatedAtBeginningConfig();
            case ActivatedTypes.Trigger:
                return new ActivatedByTriggerConfig(_activatedTriggers);
            default:
                throw new ArgumentException(nameof(_activatedType));
        }
    }

    public IDeactivatorConfig GetDeactivator()
    {
        switch (_deactivatedType)
        {
            case ActivatedTypes.AfterWhile:
                return new DeactivatedAfterWhileConfig(_deactivatedTriggers, _timeDeactivation);
            case ActivatedTypes.Beginning:
                return new DeactivateAtEndConfig();
            case ActivatedTypes.Trigger:
                return new DeactivateByTriggerConfig(_deactivatedTriggers);
            default:
                throw new ArgumentException(nameof(_deactivatedType));
        }
    }

    protected void LogErrorAndStopGame()
    {
        string errorMessage = "requiredObject is not assigned or is assigned incorrectly!";
        Debug.LogError(errorMessage, this);
    }

    private void CheckParameters()
    {
        switch (_mover)
        {
            case MoverTypes.PointByPoint:
                if (_speed <= 0 || _points == null || _points.Any(point => point == null)|| _points.Any(point=>point==Vector3.zero))
                {
                    LogErrorAndStopGame();
                }

                break;

            case MoverTypes.TargetFollower:
                if (_speed <= 0 )
                {
                    LogErrorAndStopGame();
                }

                break;
        }

        switch (_activatedType)
        {
            case ActivatedTypes.AfterWhile:
                if (_timeActivation < 0 || _activatedTriggers == null || _activatedTriggers.Any(trigger => trigger == null))
                {
                    LogErrorAndStopGame();
                }

                break;
            case ActivatedTypes.Trigger:
                if (_activatedTriggers == null || _activatedTriggers.Any(trigger => trigger == null))
                {
                    LogErrorAndStopGame();
                }

                break;
        }

        switch (_deactivatedType)
        {
            case ActivatedTypes.AfterWhile:
                if (_timeDeactivation < 0 || _deactivatedTriggers == null || _deactivatedTriggers.Any(trigger => trigger == null))
                {
                    LogErrorAndStopGame();
                }

                break;

            case ActivatedTypes.Trigger:
                if (_deactivatedTriggers == null || _deactivatedTriggers.Any(trigger => trigger == null))
                {
                    LogErrorAndStopGame();
                }

                break;
        }
    }
}
