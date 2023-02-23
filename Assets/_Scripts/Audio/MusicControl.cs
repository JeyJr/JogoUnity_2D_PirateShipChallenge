using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public List<AudioClip> musics;
    public AudioSource audioSource;
    private int currentMusicIndex;

    void Start()
    {
        UpdateMusicVolume();
        PlayRandomMusic();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    void PlayRandomMusic()
    {
        currentMusicIndex = Random.Range(0, musics.Count);
        audioSource.clip = musics[currentMusicIndex];
        audioSource.Play();
    }

    public void UpdateMusicVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("musicVolume");
    }
}
