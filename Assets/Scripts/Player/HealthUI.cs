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
        healthSystem = GameObject.FindGameObjectWithTag("GameController").GetComponent<HealthSystem>();
        hpText = GetComponent<TextMeshProUGUI>();

        hp = healthSystem.HP;
        maxHp = healthSystem.MaxHP;
        hpText.text = "HP: " + hp.ToString() + "/" + maxHp.ToString();
	}
	
	void Update () {
        hp = healthSystem.HP;
        maxHp = healthSystem.MaxHP;
        hpText.text = "HP: " + hp.ToString() + "/" + maxHp.ToString();
    }
}
