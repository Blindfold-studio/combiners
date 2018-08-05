using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour {

    private int hp;
    private int maxHp;
    private HealthSystem healthSystem;
    private TextMeshProUGUI hpText;

    void Start () {
        healthSystem = HealthSystem.instance;
        hpText = GetComponent<TextMeshProUGUI>();

        hp = healthSystem.CurrentHealth;
        maxHp = healthSystem.MaxHealth;
        hpText.text = "HP: " + hp.ToString() + "/" + maxHp.ToString();
	}
	
	void Update () {
        hp = healthSystem.CurrentHealth;
        maxHp = healthSystem.MaxHealth;
        hpText.text = "HP: " + hp.ToString() + "/" + maxHp.ToString();
    }
}
