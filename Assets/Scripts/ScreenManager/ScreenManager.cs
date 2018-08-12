using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour {

    private GameManager gameManager;
	
	void Start () {
        gameManager = GameManager.instance;
	}
	
	public void StartGame ()
    {
        gameManager.StartGame();
    }

    public void Restart ()
    {
        gameManager.RestartScene();
    }

    public void BackToMenu()
    {
        gameManager.LoadMenuScene();
    }

    public void QuitGame ()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
