using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour {
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI speedText;

    private bool isUpgrade;
    private int maxHealth;
    private int maxArrow;
    private float speed;
    private GameManager gameManager;

    void Start () {
        gameManager = GameManager.instance;

        maxHealth = gameManager.MaxHealth;
        maxArrow = gameManager.MaxArrow;
        speed = gameManager.Speed;
        isUpgrade = false;

        healthText.text = "Health : " + maxHealth.ToString();
        arrowText.text = "Arrow : " + maxArrow.ToString();
        speedText.text = "Speed : " + speed.ToString();
    }
	
	
	void Update () {
        healthText.text = "Health : " + maxHealth.ToString();
        arrowText.text = "Arrow : " + maxArrow.ToString();
        speedText.text = "Speed : " + speed.ToString();
    }

    public void UpgradeHealth()
    {
        if (!isUpgrade)
        {
            maxHealth += 1;
            isUpgrade = true;
        }        
    }

    public void UpgradeArrow()
    {
        if (!isUpgrade)
        {
            maxArrow += 1;
            isUpgrade = true;
        } 
    }

    public void UpgradeSpeed()
    {
        if (!isUpgrade)
        {
            speed += 1;
            isUpgrade = true;
        }
    }

    public void ResetAttribute ()
    {
        maxHealth = gameManager.MaxHealth;
        maxArrow = gameManager.MaxArrow;
        speed = gameManager.Speed;
        isUpgrade = false;
    }

    public void ConfirmUpgrade ()
    {
        gameManager.MaxHealth = maxHealth;
        gameManager.MaxArrow = maxArrow;
        gameManager.Speed = speed;

        Debug.Log("Confirm Upgrade and move to next level");
        Time.timeScale = 1f;
        gameManager.LoadNextScene();
    }
}
