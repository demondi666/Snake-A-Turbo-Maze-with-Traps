using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterView : MonoBehaviour
{
    private const string IsIdling = "IsIdling";
    private const string IsMoving = "IsMoving";
    private const string IsEating = "IsEating";

    private Animator _animator;

    public void Initialize()
    {
        _animator = GetComponent<Animator>();
    }

    public void StartIdling() => _animator.SetBool(IsIdling, true);
    public void StopIdling() => _animator.SetBool(IsIdling, false);

    public void StartMoving() => _animator.SetBool(IsMoving, true);
    public void StopMoving() => _animator.SetBool(IsMoving, false);

    public void StartEating()=> _animator.SetBool(IsEating, true);
    public void StopEating()=> _animator.SetBool(IsEating, false);

}
