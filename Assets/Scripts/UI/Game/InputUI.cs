using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class InputUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _group;

    private void Start()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            _group.alpha = 1;
            _group.blocksRaycasts = true;
            _group.interactable = true;
        }
        else
        {
            _group.alpha = 0;
            _group.blocksRaycasts = false;
            _group.interactable = false;
        }
    }
}
