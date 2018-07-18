using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosePanel : MonoBehaviour {

    private GameManager gameManager;

	void Start () {
        gameManager = GameManager.instance;
	}

    public void RestartGame()
    {
        gameManager.RestartScene();
    }

    public void BackToMenu()
    {
        gameManager.LoadMenuScene();
    }
}
