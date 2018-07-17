using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUpgradeUI : MonoBehaviour {
    GameManager gameManager;
    TextMeshProUGUI maxHealth;

	void Start () {
        gameManager = GameManager.instance;
        maxHealth = GetComponent<TextMeshProUGUI>();

        maxHealth.text = "Health : " + gameManager.MaxHealth.ToString();
	}

	void Update () {
        maxHealth.text = "Health : " + gameManager.MaxHealth.ToString();
    }
}
