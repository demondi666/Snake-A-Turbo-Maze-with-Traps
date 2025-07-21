public class MoveState : MovementState
{ 
    private CharacterConfig _config;

    public MoveState(IStateSwitcher stateSwitcher, StateMachineData data, Snake head) : base(stateSwitcher, data, head)
    => _config = Head.Config;

    public override void Enter()
    {
        base.Enter();
        View.StartMoving();
        Data.Speed = _config.Speed;
        Data.SpeedRotation = _config.SpeedRotation;
        Data.BonesDistance = _config.BonesDistance;
        Data.SmoothTime = _config.SmoothTimeBones;
    }

    public override void Exit()
    {
        base.Exit();
        View.StopMoving();
    }

    public override void Update()
    {
        base.Update();
    }
}
