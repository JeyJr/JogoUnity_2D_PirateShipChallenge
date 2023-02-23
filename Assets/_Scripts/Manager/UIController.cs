using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum Difficulty { Fácil, Normal, Difícil, Insano, Poseidon}
public enum Panels { mainMenu, config, HUD, endGame}
public class UIController : MonoBehaviour
{
    public List<GameObject> panelsGameObject;

    public GameManager gameManager;
    public PlayerBulletsPooling playerBullet;

    [Header("Panel Config")]
    public TextMeshProUGUI txtDuration;
    public TextMeshProUGUI txtDifficulty;
    public Slider sliderDuration, sliderDifficulty;

    [Header("Panel HUD")]
    public TextMeshProUGUI txtGameTime;
    public Slider sliderSShoot, sliderFShoot;


    [Header("Panel EndGame")]
    public TextMeshProUGUI txtPoints;


    private void Start()
    {
        sliderDuration.maxValue = 3;
        sliderDuration.minValue = 1;
        sliderDuration.value = 1;

        sliderDifficulty.maxValue = Enum.GetValues(typeof(Difficulty)).Length - 1 ;
        sliderDifficulty.minValue = 0;
        sliderDifficulty.value = 1;

        OnSliderDifficultyValueChange();
        OnSliderDurationValueChange();
    }
    
    //MainMenu
    public void BtnStartMatch() => gameManager.SetGameState(GameState.StartMatch);
    
    //Config
    public void OnSliderDifficultyValueChange()
    {
        int index = (int)sliderDifficulty.value;
        txtDifficulty.text = "Dificuldade: <color=#b342f5>" + ((Difficulty)index).ToString()+ "</color>";
        gameManager.GameDifficulty = index + 1;
    }
    public void OnSliderDurationValueChange()
    {
        txtDuration.text = "Duração: <color=#f55a42>" + sliderDuration.value + "</color> minutos";
        gameManager.GameTime = sliderDuration.value * 60;
    }

    //HUD
    public void UpdateTextGameTime(float value)
    {
        int minutes = Mathf.FloorToInt(value / 60);
        int seconds = Mathf.FloorToInt(value % 60);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        txtGameTime.text = timeString;
    }


    public void StartCountdownToSideShot(float delayTime) => StartCoroutine(SideShot(delayTime));
    IEnumerator SideShot(float delayTime)
    {
        sliderSShoot.maxValue = delayTime;
        sliderSShoot.minValue = 0;
        sliderSShoot.value = delayTime;

        while(delayTime > 0)
        {
            yield return new WaitForSeconds(.1f);
            delayTime-= .1f;
            sliderSShoot.value = delayTime;
        }

        yield return new WaitForEndOfFrame();
        playerBullet.IsShootingS = false;
    }
    public void StartCountdownToFrontaleShot(float delayTime) => StartCoroutine(FrontalShot(delayTime));
    IEnumerator FrontalShot(float delayTime)
    {
        sliderFShoot.maxValue = delayTime;
        sliderFShoot.minValue = 0;
        sliderFShoot.value = delayTime;

        while (delayTime > 0)
        {
            yield return new WaitForSeconds(.1f);
            delayTime -= .1f;
            sliderFShoot.value = delayTime;
        }
        playerBullet.IsShootingF = false;
    }


    //EndGame
    public void BtnMainMenu() => gameManager.SetGameState(GameState.MainMenu);
    public void ShowPointsInEndGame(float points)
    {
        txtPoints.text = $"Barcos destruídos: \n <color=#e8134f> {points} </color>";
    }

    public void SetPanelActive(GameObject obj) => obj.SetActive(!obj.activeSelf);
    public void SetPanelsActive(Panels panel)
    {
        int index = (int)panel;

        for (int i = 0; i < panelsGameObject.Count; i++)
        {
            panelsGameObject[i].SetActive(i == index);
        }
    }


}
