using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    public TextMeshProUGUI txtMusicVolume;
    public TextMeshProUGUI txtSFXVolume;

    public Slider sliderMusic;
    public Slider sliderSFX;

    private const string musicVolume = "musicVolume";
    private const string sfxVolume = "sfxVolume";

    public MusicControl musicControl;
    public UISFX uiSFX;
    public BoatsSFX playerSFX;

    private void Start()
    {
        if (musicControl == null)
            musicControl = FindObjectOfType<MusicControl>();

        if (!PlayerPrefs.HasKey(musicVolume))
            PlayerPrefs.SetFloat(musicVolume, 0.3f);

        if (!PlayerPrefs.HasKey(sfxVolume))
            PlayerPrefs.SetFloat(sfxVolume, 0.5f);

        sliderMusic.value = PlayerPrefs.GetFloat(musicVolume);
        sliderSFX.value = PlayerPrefs.GetFloat(sfxVolume);

        txtMusicVolume.text = "Music: " + (sliderMusic.value * 100).ToString("F2") + "%";
        txtSFXVolume.text = "SFX: " + (sliderSFX.value * 100).ToString("F2") + "%";
    }

    public void SetMusicVolume()
    {
        txtMusicVolume.text = "Music: " + (sliderMusic.value * 100).ToString("F2") + "%";
        PlayerPrefs.SetFloat(musicVolume, sliderMusic.value);

        musicControl.UpdateMusicVolume();
    }

    public void SetSFXVolume()
    {
        txtSFXVolume.text = "SFX: " + (sliderSFX.value * 100).ToString("F2") + "%";
        PlayerPrefs.SetFloat(sfxVolume, sliderSFX.value);

        uiSFX.UpdateSFXVolume();
        playerSFX.UpdateSFXVolume();
    }
}
