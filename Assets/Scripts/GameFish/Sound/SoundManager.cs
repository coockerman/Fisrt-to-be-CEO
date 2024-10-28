using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : ASingleton<SoundManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioMusic;

    public void PlayAudioSource(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayGameOverSound(AudioClip audioClip)
    {
        audioMusic.loop = false;
        audioMusic.PlayOneShot(audioClip);
    }

    public string ChangeVolumeAudioSource()
    {
        if (audioSource.mute == false)
        {
            audioSource.mute = true;
            return "SFX: OFF";
        }
        else
        {
            audioSource.mute = false;
            return "SFX: ON";
        }
    }
    public string ChangeVolumeAudioMusic()
    {
        if (audioMusic.mute == false)
        {
            audioMusic.mute = true;
            return "MUSIC: OFF";
        }
        else
        {
            audioMusic.mute = false;
            return "MUSIC: ON";
        }
    }
}
