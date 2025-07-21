using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Trap
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator?.SetBool("IsWalk", true);
    }

    private void OnDisable()
    {
        _animator?.SetBool("IsWalk", false);
    }

    protected override void Animate()
    {
        
    }
}
