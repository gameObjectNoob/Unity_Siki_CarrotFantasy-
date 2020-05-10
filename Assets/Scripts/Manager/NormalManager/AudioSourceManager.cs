using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 负责控制音乐的播放和停止以及游戏中各种音效的播放
/// </summary>
public class AudioSourceManager
{
    private AudioSource[] audioSources;//0播放BG 1播放音效
    private bool playEffectMusic = true;
    private bool playBGMusic = true;

    public AudioSourceManager()
    {
        audioSources = GameManager.Instance.GetComponents<AudioSource>();
    }

    //播放背景音乐
    public void PlayBGMusic(AudioClip audioClip) {
        if (!audioSources[0].isPlaying||audioSources[0].clip==audioClip)
        {
            audioSources[0].clip = audioClip;
            audioSources[0].Play();
        }
    }

    //播放音效
    public void PlayEffectMusice(AudioClip audioClip) {
        if (playEffectMusic)
        {
            audioSources[1].PlayOneShot(audioClip);
        }
    }

    //关闭背景音乐
    public void CloseBGMusic()
    {
        audioSources[0].Stop();
    }
    //打开背景音乐
    public void OpenBGMusic()
    {
        audioSources[0].Play();
    }

    public void closeOrOpenBGMusic()
    {
        playBGMusic = !playBGMusic;
        if (playBGMusic)
        {
            OpenBGMusic();
        }
        else
        {
            CloseBGMusic();
        }
    }
    public void closeOrOpenEffectMusic()
    {
        playEffectMusic = !playEffectMusic;
    }


}





