using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Booster
{
    protected override void EventHappened(Snake snake)
    {
        snake.OnPickUpCoin.Invoke();
        gameObject.SetActive(false);
    }
}
