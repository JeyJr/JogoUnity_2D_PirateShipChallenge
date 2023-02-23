using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState{MainMenu, StartMatch,  EndGame}
public class GameManager : MonoBehaviour
{
    public GameState GameState { get; set; }

    [SerializeField] private UIController uiController;
    [SerializeField] private SpawnEnemies spawnEnemies;
    [SerializeField] private BoatStats player;

    private float gameTime;
    public float GameTime
    {
        get => gameTime;
        set => gameTime = value;
    }

    private int gameDifficulty;
    public int GameDifficulty
    {
        get => gameDifficulty;
        set
        {
            if (gameDifficulty == value)
                return;

            gameDifficulty = value;
            spawnEnemies.MaxEnemiesToSpawn = 3 + (gameDifficulty * 3);

            float delayTime = 2;
            spawnEnemies.DelayTimeToSpawn = delayTime - (gameDifficulty * .3f);
        }
    }

    private int points;
    public int Points
    {
        get => points;
        set => points = value;
    }


    private void Awake()
    {
        player.GetComponent<BoatStats>().GameManager = this;
        SetGameState(GameState.MainMenu);
    }

    public void SetGameState(GameState newState)
    {
        GameState = newState;
        switch (newState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.StartMatch:
                StartMatch();
                break;
            case GameState.EndGame:
                EndGame();
                break;
            default:
                throw new ArgumentException($"Invalid game state: {newState}");
        }
    }



    private void MainMenu()
    {
        spawnEnemies.StopSpawn = true;
        spawnEnemies.ResetQueue();

        uiController.SetPanelsActive(Panels.mainMenu);

        PlayerInitialSetup();
    }


    private void StartMatch()
    {
        StopCoroutine(UpdateGameTime());

        PlayerInitialSetup();

        Points = 0;
        uiController.SetPanelsActive(Panels.HUD);
        StartCoroutine(UpdateGameTime());

        spawnEnemies.StopSpawn = false;
        spawnEnemies.StartSpawnEnemies();
    }

    private void EndGame()
    {
        spawnEnemies.StopSpawn = true;
        PlayerInitialSetup();
        uiController.SetPanelsActive(Panels.endGame);
        uiController.ShowPointsInEndGame(Points);
        spawnEnemies.ResetQueue();
    }

    IEnumerator UpdateGameTime()
    {
        float time = GameTime;

        while (time > 0)
        {
            uiController.UpdateTextGameTime(time);
            time--;

            if (GameState == GameState.EndGame)
                time = 0;
            yield return new WaitForSeconds(1);
        }

        if (GameState == GameState.StartMatch)
            SetGameState(GameState.EndGame);
    }

    void PlayerInitialSetup()
    {
        float maxLife = 20;
        player.SetInitialValues(maxLife);

        player.GetComponent<Transform>().position = new Vector3(0f, 0f, -0.5f);
        player.GetComponent<Transform>().rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
