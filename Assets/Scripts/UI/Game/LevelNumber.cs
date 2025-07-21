using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumber : UI
{
    [SerializeField] private List<Sprite> _levelNumberSprites;
    [SerializeField] private Image _image;

    public void ChangeLevelNumber(int levelNumber)
    {
        if(levelNumber<_levelNumberSprites.Count)
            _image.sprite = _levelNumberSprites[levelNumber];
    }

    public override void Close()
    {
        CanvasGroup.alpha = 0;
    }

    public override void Open()
    {
        CanvasGroup.alpha = 1;
    }
}
