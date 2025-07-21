using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly IStateSwitcher StateSwitcher;
    protected readonly StateMachineData Data;
    protected readonly Snake Head;
    protected readonly CharacterView View;

    private const float RateOfChange—oefficient = 2f;

    private List<Vector3> _velocitiesBones = new List<Vector3>();

    protected MovementState(IStateSwitcher stateSwitcher, StateMachineData data, Snake head)
    {
        StateSwitcher = stateSwitcher;
        Data = data;
        Head = head;
        View = head.View;
    }

    protected Input Input => Head.InputSnake;
    protected CharacterController Controller => Head.Controller;

    public virtual void Enter()
    {
        Debug.Log(GetType());

        if(IsSubscribed(Head.OnDied, new Action(Die))==false)
            Head.OnDied += Die;

        if(IsSubscribed(Head.Eating, new Action(Eat)) == false)
            Head.Eating += Eat;

        if (IsSubscribed(Head.WalkedThroughDoor, new Action(OnWalkedThroughDoor)) == false)
            Head.WalkedThroughDoor += OnWalkedThroughDoor;

        if (IsSubscribed(Head.OnSpeedChange, new Action<bool> (MovementChange)) == false)
            Head.OnSpeedChange += MovementChange;
    }

    public virtual void Exit()
    {
        Head.Eating -= Eat;
        Head.OnDied -= Die;
        Head.WalkedThroughDoor -= OnWalkedThroughDoor;
        Head.OnSpeedChange -= MovementChange;
    }

    public void HandleInput()
    {
        Data.XInput = ReadInput();
        Rotate(Data.SpeedRotation);
    }
    public virtual void Update()
    {
        MoveHead();
        MoveTail();
    }

    protected bool IsInputZero() => Data.XInput == 0;

    private void MovementChange(bool onSpeedUp)
    {
        if (onSpeedUp == true)
        {
            Data.Speed *= RateOfChange—oefficient;
            Data.SmoothTime /= RateOfChange—oefficient;
        }
        else
        {
            Data.Speed /= RateOfChange—oefficient;
            Data.SmoothTime *= RateOfChange—oefficient;
        }
    }
    

    private void Rotate(float speed)
    {
        float angle = ReadInput() * speed * Time.deltaTime;
        Controller.transform.Rotate(0, angle, 0);
    }

    private void MoveHead()
    {
        Controller.transform.position = Controller.transform.position + Controller.transform.forward * Data.Speed * Time.deltaTime;
    }

    private void MoveTail()
    {
        Vector3 previousPosition = Controller.transform.position;

        if (Head.Tails != null)
        {
            for (int i= 0; i< Head.Tails.Count; i++)
            {
                if (Vector3.Distance(Head.Tails[i].transform.position, previousPosition) > Data.BonesDistance)
                {
                    Bone bone = Head.Tails[i];
                    Vector3 currentBonePosition = bone.transform.position;

                    if (_velocitiesBones.Count <= i)
                    {
                        _velocitiesBones.Add(Vector3.zero); 
                    }

                    Vector3 tempVelocity = _velocitiesBones[i];
                    bone.transform.position = Vector3.SmoothDamp(bone.transform.position, previousPosition, ref tempVelocity, Data.SmoothTime, Data.Speed*Data.Speed);
                    RotateBone(bone.transform, previousPosition);
                    _velocitiesBones[i] = tempVelocity;
                    previousPosition = currentBonePosition;
                }
                else
                {
                    break;
                }
            }
        }
    }

    private void RotateBone(Transform bone ,Vector3 position)
    {
        Vector3 direction = position - bone.position;

        bone.rotation = Quaternion.LookRotation(direction);
    }

    private float ReadInput() => Input.Movement.Move.ReadValue<float>();

    private bool IsSubscribed(Delegate @event, Delegate handler)
    {
        if (@event == null || handler == null)
            return false;

        return @event.GetInvocationList().Contains(handler);
    }

    private void Eat()
    {
        StateSwitcher.SwitchState<EatState>();
    }

    private void Die()
    {
        StateSwitcher.SwitchState<DeadState>();
    }

    private void OnWalkedThroughDoor()
    {
        StateSwitcher.SwitchState<DoorOpeningState>();
    }
}
