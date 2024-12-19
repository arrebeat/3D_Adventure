using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MuteToggleHelper : MonoBehaviour
{
    public Toggle toggle;
    public AudioMixer mixerBase;
    
    void Start()
    {
    }
    
    public void ToggleMute(bool isMuted)
    {
        SoundManager.instance.musicSource.mute = !isMuted;
        SoundManager.instance.sfxSource.mute = !isMuted;

        foreach (var audioSource in SfxPool.instance.audioSourceList)
        {
            audioSource.mute = !isMuted;
        } 
    }
}
