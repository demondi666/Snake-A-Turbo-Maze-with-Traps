public class IdlingState : MovementState
{
    public IdlingState(IStateSwitcher stateSwitcher, StateMachineData data, Snake head) : base(stateSwitcher, data, head)
    {
    }

    public override void Enter()
    {
        base.Enter();
        View.StartIdling();
        Data.Speed = 0;
    }

    public override void Exit()
    {
        base.Exit();
        View.StopIdling();
    }

    public override void Update()
    {
        base.Update();

        if (IsInputZero())
        {
            return;
        }
        StateSwitcher.SwitchState<MoveState>();
    }
}
