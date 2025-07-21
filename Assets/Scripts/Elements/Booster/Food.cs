using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Booster
{
    protected override void EventHappened(Snake snake)
    {
        snake?.Eating();
        gameObject.SetActive(false);
    }
}
