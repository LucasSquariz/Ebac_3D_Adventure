using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;

public class AudioManager : Singleton<AudioManager>
{
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;
    public AudioSource musicSource;

    public void PlayMusicByType(MusicType type)
    {
        var music = GetMusicByType(type);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType type)
    {
        return musicSetups.Find(i => i.musicType == type);        
    }

    public SFXSetup GetSFXByType(SFXType type)
    {
        return sfxSetups.Find(i => i.sfxType == type);
    }

}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}