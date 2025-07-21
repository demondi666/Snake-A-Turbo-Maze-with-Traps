using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public abstract class Effector : MonoBehaviour, IMovable
{
    private IMover _mover;

    protected AudioSource AudioSource;
    protected Model Model;

    public Transform Transform => transform;

    public UnityAction Action;

    private void Awake()
    {
        Model = GetComponentInChildren<Model>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _mover?.Update();
    }

    public void SetMover(IMover mover)
    {
        _mover?.StopMove();
        _mover = mover;
        _mover.StartMove();
    }

    public void SetActive(IActivated activated)
    {
        activated.Activate();
    }

    public void SetDeactivator(IDeactivated deactivated)
    {
        deactivated.Deactivate();
    }

    protected abstract void Animate();
}
