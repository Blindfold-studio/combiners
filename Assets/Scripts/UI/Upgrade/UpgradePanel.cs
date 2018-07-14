using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePanel : MonoBehaviour {
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI speedText;

    GameManager gameManager;

    void Start () {
        gameManager = GameManager.instance;

        healthText.text = "Health : " + gameManager.MaxHealth.ToString();
        arrowText.text = "Arrow : " + gameManager.MaxArrow.ToString();
        speedText.text = "Speed : " + gameManager.Speed.ToString();
    }
	
	
	void Update () {
        healthText.text = "Health : " + gameManager.MaxHealth.ToString();
        arrowText.text = "Arrow : " + gameManager.MaxArrow.ToString();
        speedText.text = "Speed : " + gameManager.Speed.ToString();
    }
}
