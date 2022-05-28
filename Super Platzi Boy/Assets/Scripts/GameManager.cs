using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { menu, inGame, gameOver }

public class GameManager : MonoBehaviour
{
    public GameState state = GameState.menu;

    public static GameManager instance;

    PlayerController playerController;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if((state == GameState.menu && Input.GetButtonDown("Submit")) || (state == GameState.inGame && Input.GetKeyDown(KeyCode.R))) {
            StartGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {

        }
        else if(newGameState == GameState.inGame)
        {
            playerController.StartGame();
        }
        else if(newGameState == GameState.gameOver)
        {

        }
        this.state = newGameState;
    }
}
