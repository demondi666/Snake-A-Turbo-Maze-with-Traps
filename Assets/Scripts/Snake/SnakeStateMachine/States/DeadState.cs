using UnityEngine;

public class DeadState : IState
{
    private const float TimeToDestruction = 4f;

    private readonly IStateSwitcher _stateSwitcher;
    private readonly DestructibleModel _model;
    private readonly Snake _head;

    private DestructibleModel _createdModel;
    
    public DeadState(IStateSwitcher stateSwitcher, Snake head)
    {
        _stateSwitcher = stateSwitcher;
        _head = head;
        _model = head.DestructibleModel;
    }

    public void Enter()
    {
        Die();
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

    private void Die ()
    {
        _createdModel = MonoBehaviour.Instantiate(_model,_head.transform.position , _head.transform.rotation);
        _head.gameObject.SetActive(false);
        MonoBehaviour.Destroy(_createdModel.gameObject, TimeToDestruction);
    }
}
