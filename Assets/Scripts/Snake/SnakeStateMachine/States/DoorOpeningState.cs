using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningState : IState
{
    private readonly Snake _head;
    private readonly IStateSwitcher _stateSwitcher;

    public DoorOpeningState(IStateSwitcher stateSwitcher, Snake head)
    {
        _head = head;
        _stateSwitcher = stateSwitcher;
    }

    public void Enter()
    {
        Debug.Log(GetType());
        _head.gameObject.SetActive(false);
    }

    public void Exit()
    {
        
    }

    public void HandleInput()
    {
        
    }

    public void Update()
    {
        _stateSwitcher.SwitchState<IdlingState>();
    }
}
