using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;

    [Header("Setup")]
    public Color flashColor = Color.white;
    public float flashDuration = .1f;

    private Color _defaultColor;
    private Tween _currentTween;

    void OnValidate()
    {
        if (meshRenderer == null) meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (skinnedMeshRenderer == null) skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    } 

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (meshRenderer != null && !_currentTween.IsActive())
        _currentTween = meshRenderer.material.DOColor(flashColor, "_EmissionColor", flashDuration).SetLoops(2, LoopType.Yoyo);
        
        if (skinnedMeshRenderer != null && !_currentTween.IsActive())
        _currentTween = skinnedMeshRenderer.material.DOColor(flashColor, "_EmissionColor", flashDuration).SetLoops(2, LoopType.Yoyo);
    }
}
