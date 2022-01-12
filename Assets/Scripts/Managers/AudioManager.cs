using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Audio Manager Is Null");
            return _instance;
        }
    }
    
    public AudioSource _audioSource;
    [SerializeField] private AudioClip _regularMusic;
    [SerializeField] private AudioClip _victoryMusic;

    private void Awake()
    {
        _instance = this;
    }

    public void ChangeClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    public void PlayOneShot(AudioClip clip, float volume)
    {
        _audioSource.PlayOneShot(clip, volume);
    }

    public void ChangeToRegularMusic()
    {
        if (_audioSource.clip == _regularMusic)
            return;
        ChangeClip(_regularMusic);
    }

    public void PlayVictoryMusic()
    {
        ChangeClip(_victoryMusic);
    }
}
