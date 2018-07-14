using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAttribute : MonoBehaviour {
    GameManager gameManager;

	void Start () {
        gameManager = GameManager.instance;
	}
	
	public void UpgradeHealth ()
    {
        gameManager.MaxHealth = 1;
    }

    public void UpgradeArrow()
    {
        gameManager.MaxArrow = 1;
    }

    public void UpgradeSpeed()
    {
        gameManager.Speed = 1;
    }
}
