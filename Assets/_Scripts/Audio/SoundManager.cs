using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.Singleton;

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

public enum SfxType
{
    NONE,
    COIN,
    JUMP,
    SHOOT,
    CHECKPOINT
}

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public List<MusicSetup> musicSetups;
    public List<SfxSetup> sfxSetups;

    private AudioClip _queuedMusic;
    private AudioClip _queuedSfx;

    public void PlayMusicByType(MusicType musicType)
    {
        var setup = GetMusicSetupByType(musicType);
        
        _queuedMusic = setup.audioClip;
        musicSource.clip = _queuedMusic;
        musicSource.Play();
    }
    public MusicSetup GetMusicSetupByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public void PlaySfxByType(SfxType sfxType)
    {
        var setup = GetSfxSetupByType(sfxType);
        Debug.Log("CARAMBA" + setup);

        _queuedSfx = setup.audioClip;
        sfxSource.clip = _queuedSfx;
        sfxSource.Play();
    }
    public SfxSetup GetSfxSetupByType(SfxType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

[System.Serializable]
public class SfxSetup
{
    public SfxType sfxType;
    public AudioClip audioClip;
}
