using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { menu, inGame, gameOver }

public class GameManager : MonoBehaviour
{
    public GameState state = GameState.menu;

    public static GameManager instance;

    AudioSource backgroundAudio;
    PlayerController playerController;

    public int totalObjects = 5;
    public int collectedObjects = 0;

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
        backgroundAudio = GetComponent<AudioSource>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        GameView.instance.Hide();
        CreditsView.instance.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit")) {
            StartGame();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            MenuManager.instance.ExitGame();
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
            backgroundAudio.Stop();
            playerController.StartGame();
            GameView.instance.Hide();
            CreditsView.instance.Hide();
            MenuManager.instance.ShowMainMenu();
        }
        else if(newGameState == GameState.inGame)
        {
            backgroundAudio.Play();
            playerController.StartGame();
            MenuManager.instance.HideMainMenu();
            GameView.instance.Show();
        }
        else if(newGameState == GameState.gameOver)
        {
            backgroundAudio.Stop();
            GameView.instance.Hide();
            CreditsView.instance.Show();
            playerController.StartGame();
        }
        this.state = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObjects += collectable.value;
        if(collectedObjects >= totalObjects)
        {
            Invoke("GameOver", 5);
        }
    }
}
