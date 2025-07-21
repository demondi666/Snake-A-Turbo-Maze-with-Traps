using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : Booster
{
    protected override void EventHappened(Snake snake)
    {
        snake?.OnSpeedChange(true);
        gameObject.SetActive(false);
    }
}
