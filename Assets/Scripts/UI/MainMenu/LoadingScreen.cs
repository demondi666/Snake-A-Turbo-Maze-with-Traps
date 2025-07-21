using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using IJunior.TypedScenes;
using YG;
using System;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private List<LoadingElement> _elementsToShow;
    [SerializeField] private float _finalDelay = 1f;
    [SerializeField] Image _fadeImage;

    private int _currentShownElements;
    private CanvasGroup _canvasGroup;
    private AsyncOperation _operation;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void LoadLevel(Level level)
    {
        YandexGame.FullscreenShow();
        Open();
        _operation = Game.LoadAsync(level);
        StartCoroutine(LoadGameScene(_operation));
    }

    public void LoadMenu()
    {
        YandexGame.FullscreenShow();
        Open();
        _operation = Menu.LoadAsync();
        StartCoroutine(LoadGameScene(_operation));
    }

    private IEnumerator LoadGameScene(AsyncOperation operation) 
    { 
        operation.allowSceneActivation = false;

        HideAllElements();

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            UpdateElementsVisibility(progress);
            

            if (progress >= 0.999f)
            {
                yield return StartCoroutine(FadeOutScreen());
                yield return new WaitForSeconds(_finalDelay);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private void HideAllElements()
    {
        foreach (var element in _elementsToShow)
        {
            element.gameObject.SetActive(false);
        }
    }

    private void UpdateElementsVisibility(float progress)
    {
        int shouldShowCount = Mathf.FloorToInt(progress * _elementsToShow.Count);
        shouldShowCount = Mathf.Clamp(shouldShowCount, 0, _elementsToShow.Count);

        while (_currentShownElements < shouldShowCount)
        {
            if (_currentShownElements < _elementsToShow.Count)
            {
                _elementsToShow[_currentShownElements].gameObject.SetActive(true);
            }

            _currentShownElements++;
        }
    }

    private IEnumerator FadeOutScreen()
    {
        float elapsed = 0f;

        while (elapsed < _finalDelay)
        {
            _fadeImage.color = Color.Lerp(Color.clear, Color.black, elapsed / _finalDelay);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private void Open()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.interactable = true;
    }
}
