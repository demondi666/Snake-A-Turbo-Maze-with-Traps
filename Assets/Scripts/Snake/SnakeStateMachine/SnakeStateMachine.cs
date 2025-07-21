using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeStateMachine : IStateSwitcher
{
    private IState _currentState;
    private List<IState> _states;

    public SnakeStateMachine(Snake head)
    {
        StateMachineData stateMachineData = new StateMachineData();
        _states = new List<IState>()
        {
            new IdlingState(this, stateMachineData,  head),
            new MoveState(this, stateMachineData, head),
            new EatState(this,stateMachineData , head),
            new DeadState(this, head), 
            new DoorOpeningState(this, head)
        };
        _currentState = _states[0];
        _currentState.Enter();
    }

    public void SwitchState<T>() where T : IState
    {
        IState state = _states.FirstOrDefault(state => state is T);

        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void HandleInput() => _currentState.HandleInput();

    public void Update() => _currentState.Update();

}
