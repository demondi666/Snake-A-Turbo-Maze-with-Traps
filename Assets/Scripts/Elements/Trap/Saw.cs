using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saw : Trap
{
    [SerializeField] private float _rotationSpeed;

    private void OnEnable()
    {
        SavePosition();
    }

    private void OnDisable()
    {
        ResetPosition();
    }

    private void FixedUpdate()
    {
        Animate();
    }

    protected override void Animate()
    {
        Model.transform.Rotate(Vector3.back, _rotationSpeed * Time.fixedDeltaTime);
    }
}
