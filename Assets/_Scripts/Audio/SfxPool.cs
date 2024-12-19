using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArreTools.Singleton;

public class SfxPool : Singleton<SfxPool>
{
    public int poolSize = 10;

    public List<AudioSource> audioSourceList;
    private int _index = 0;

    void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        audioSourceList = new List<AudioSource>();
        
        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourcePoolItem();
        }
    }

    private void CreateAudioSourcePoolItem()
    {
        GameObject poolObj = new GameObject("Sfx_PoolItem");
        poolObj.transform.SetParent(transform);
        audioSourceList.Add(poolObj.AddComponent<AudioSource>());
    }

    public void Play(SfxType sfxType)
    {
        if (sfxType == SfxType.NONE) return;
        
        var sfx = SoundManager.instance.GetSfxSetupByType(sfxType);
        Debug.Log(sfx);
        audioSourceList[_index].clip = sfx.audioClip;
        audioSourceList[_index].Play();

        _index ++;
        if (_index >= audioSourceList.Count) _index = 0;
    }
}
