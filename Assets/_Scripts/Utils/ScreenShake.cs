using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera;
    public float shakeAmplitude;
    public float shakeFrequency;
    public float shakeDuration;

    //[SerializeField] private float _duration;

    private CinemachineBasicMultiChannelPerlin _shake;
    private Coroutine _coroutine;

    [NaughtyAttributes.Button]
    public void CameraShake()
    {
        Shake(shakeAmplitude, shakeFrequency, shakeDuration);

        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ShakeCoroutine());
    }
    
    public void Shake(float amplitude, float frequency, float duration)
    {
        _shake = playerCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _shake.m_AmplitudeGain = amplitude;
        _shake.m_FrequencyGain = frequency;

        //_duration = duration;
    }

    public IEnumerator ShakeCoroutine()
    {
        float time = 0f;

        while (time < shakeDuration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        _shake.m_AmplitudeGain = 0;
        _shake.m_FrequencyGain = 0;
    }
}
