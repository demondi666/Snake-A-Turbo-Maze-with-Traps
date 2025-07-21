using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : Booster
{
    protected override void EventHappened(Snake snake)
    {
        snake.OnSpeedChange(false);
        gameObject.SetActive(false);
    }
}
