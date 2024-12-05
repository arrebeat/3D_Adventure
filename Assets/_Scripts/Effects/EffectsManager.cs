using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectsManager : MonoBehaviour
{
    public Volume volume;
    [SerializeField] private Vignette _vignette;

    public float duration = 1f;

    private Coroutine _coroutine;

    [NaughtyAttributes.Button]
    public void VignetteEffect()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(FlashColorVignette());
    }

    public IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (volume.profile.TryGet<Vignette>(out tmp))
        {
            _vignette = tmp;
        }
        
        var c = new ColorParameter(Color.white);

        float time = 0f;

        while (time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            time += Time.deltaTime;
            _vignette.intensity.Override(Mathf.Lerp(0.3f, 0.4f, time / duration));
            _vignette.color.Override(c.value);
            yield return null;
        }

        time = 0f;

        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            time += Time.deltaTime;
            _vignette.intensity.Override(Mathf.Lerp(0.4f, 0.3f, time / duration));
            _vignette.color.Override(c.value);
            yield return null;
        }
    }
}
