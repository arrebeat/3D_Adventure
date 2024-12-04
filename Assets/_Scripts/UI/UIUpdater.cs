using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIUpdater : MonoBehaviour
{
    public Image uiImage;

    [Space(10)]
    public float updateDuration = 1f;
    public Ease ease = Ease.OutBack;

    private Tween _currentTween;

    public void UpdateValue(float f)
    {
        uiImage.fillAmount = f;
    }

    public void UpdateValue(float current, float max, bool invert)
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
        }
        
        if (invert)
        {
            _currentTween = uiImage.DOFillAmount(1 - (current / max), updateDuration).SetEase(ease);
        }
        else if (!invert)
        {
            _currentTween = uiImage.DOFillAmount(current / max, updateDuration).SetEase(ease);
        }
    }
}
