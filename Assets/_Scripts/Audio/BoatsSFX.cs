using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoatSFXClip
{
    shoot,
    takeDMG
}
public class BoatsSFX : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;
    private Dictionary<BoatSFXClip, AudioClip> clips;
    private void Start()
    {
        clips = new Dictionary<BoatSFXClip, AudioClip>
        {
            {BoatSFXClip.shoot, audioClips[0]},
            {BoatSFXClip.takeDMG, audioClips[1]},
        };
    }

    private void OnEnable()
    {
        UpdateSFXVolume();
    }

    public void PlayClip(BoatSFXClip clip)
    {
        if (clips.ContainsKey(clip))
        {
            audioSource.PlayOneShot(clips[clip]);
        }
        else
        {
            throw new ArgumentNullException($" Clip de áudio não encontrado: {clip}");
        }
    }

    public void UpdateSFXVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("sfxVolume");
    }
}
