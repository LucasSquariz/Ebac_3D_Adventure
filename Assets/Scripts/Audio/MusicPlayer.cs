using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource musicSource;

    private MusicSetup _currMusicSetup;


    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        _currMusicSetup = AudioManager.Instance.GetMusicByType(musicType);
        musicSource.clip = _currMusicSetup.audioClip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        _currMusicSetup = AudioManager.Instance.GetMusicByType(musicType);
        musicSource.clip = _currMusicSetup.audioClip;
        musicSource.Stop();
    }
}
