using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UISFXClip
{
    btn,
    btnStartMatch
}
public class UISFX : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;
    private Dictionary<UISFXClip, AudioClip> clips;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        clips = new Dictionary<UISFXClip, AudioClip>
        {
            {UISFXClip.btn, audioClips[0]},
            {UISFXClip.btnStartMatch, audioClips[1]},
        };
    }

    private void OnEnable()
    {
        UpdateSFXVolume();
    }

    public void PlayBtnStandard()
    {
        audioSource.PlayOneShot(clips[UISFXClip.btn]);
    }

    public void PlayBtnStartMatch()
    {
        audioSource.PlayOneShot(clips[UISFXClip.btnStartMatch]);
    }

    public void UpdateSFXVolume()
    {
        audioSource.volume = PlayerPrefs.GetFloat("sfxVolume");
    }
}
