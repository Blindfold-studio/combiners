using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAttribute : MonoBehaviour {

    private int maxHealth;
    private int maxArrow;
    private float speed;
    private GameManager gameManager;

	void Start () {
        gameManager = GameManager.instance;

        maxHealth = gameManager.MaxHealth;
        maxArrow = gameManager.MaxArrow;
        speed = gameManager.Speed;
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
