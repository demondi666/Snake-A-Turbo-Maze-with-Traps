using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class FragmentSplit : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Rigidbody Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
    }

    public void Activate()
    {
        _rigidbody.isKinematic = false;
    }
}
