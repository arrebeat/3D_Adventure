using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource audioSource;

    private MusicSetup _currentMusicSetup;

    void Start()
    {
        Play();
    }

    public void Play()
    {
        _currentMusicSetup = SoundManager.instance.GetMusicSetupByType(musicType);
        audioSource.clip = _currentMusicSetup.audioClip;
        audioSource.Play();
    }
}
