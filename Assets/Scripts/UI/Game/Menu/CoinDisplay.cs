using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> _coins;

    public void CoinsChange()
    {
        foreach (var coin in _coins)
        {
            if (coin.gameObject.activeSelf == false)
            {
                coin.gameObject.SetActive(true);
                break;
            }
        }
    }
}
