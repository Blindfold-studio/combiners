using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour {

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }

    public void RestartGame()
    {
        gameManager.StartGame();
    }

    public void BackToMenu()
    {
        gameManager.LoadMenuScene();
    }
}
