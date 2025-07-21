using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatState : MovementState, IState
{
    private Coroutine _eating;

    public EatState(IStateSwitcher stateSwitcher, StateMachineData data, Snake head) : base(stateSwitcher, data, head)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Head.AudioSource.PlayOneShot(Head.SoundOfEating);
        View.StartEating();
       
        if (_eating == null)
        {
            _eating = CoroutineRunner.Instance.ActivateCoroutine(Eat());
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    private IEnumerator Eat()
    {
        yield return new WaitForSeconds(1f);
        View.StopEating();
        StateSwitcher.SwitchState<MoveState>();
        _eating = null;
    }
}
