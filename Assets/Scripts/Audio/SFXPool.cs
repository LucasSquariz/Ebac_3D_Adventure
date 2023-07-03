using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    public int poolSize = 10;
    public bool _stopSFX = false;

    private List<AudioSource> _audioSourceList;
    private int _index = 0;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();
        
        for(int i = 0;  i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject pool = new GameObject("SFXPool");
        pool.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(pool.AddComponent<AudioSource>());
    }

    public void PlayByType(SFXType type)
    {
        if (type == SFXType.NONE) return;        
        var sfx = AudioManager.Instance.GetSFXByType(type);
        _audioSourceList[_index].clip = sfx.audioClip;
        if (!_stopSFX) _audioSourceList[_index].Play();
        _index++;
        if (_index >= _audioSourceList.Count) _index = 0;
               
    }

    public void TurnOffSFX()
    {
        _stopSFX = true;        
    }

    public void TurnOnSFX()
    {
        _stopSFX = false;        
    }
}
