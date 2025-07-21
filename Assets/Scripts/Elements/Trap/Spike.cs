using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Trap
{
    private void OnEnable()
    {
        SavePosition();
    }

    private void OnDisable()
    {
        ResetPosition();
    }
    protected override void Animate()
    {

    }
}
