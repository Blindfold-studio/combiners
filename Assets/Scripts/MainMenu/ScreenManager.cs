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
        gameManager.LoadNextScene();
    }

    public void QuitGame ()
    {
        Debug.Log("Exit game");
        Application.Quit();
    }
}
