using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Audio Source")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip bulletSound;
    public AudioClip deadInvadersSound;
    public AudioClip gameOverSound;
    public AudioClip playerDeadSound;
    public AudioClip backGroundMusic;

    private void Start()
    {
        audioSource.clip = backGroundMusic;
        audioSource.Play();
    }

    public void PlaySFX(AudioClip clip) 
    {
        SFXSource.PlayOneShot(clip);
    }
}
