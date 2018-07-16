using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour {

    public Image bossHpBar;
    public TextMeshProUGUI hp1;
    public TextMeshProUGUI hp2;
    private float max_health = 3f;
    private float current_health;
    private Boss3Movement boss3Movement;

    public static event Action SwapingEvent;
    public static event Action DeathEvent;

    public float Health {
        get {
            return current_health;
        }

        set {
            current_health += value;
            CheckingBossHealth();
        }
    }

	// Use this for initialization
	void Start () {
		current_health = max_health;
        boss3Movement = GetComponent<Boss3Movement>();
        UpdateHpText();
	}
	
	void CheckingBossHealth() {
        UpdateHpText();
        if(current_health <= 0) {
            if(DeathEvent != null) {
                DeathEvent();
            }
        } else if(current_health % (max_health/3) == 0 && current_health < max_health) {    
            // SwapBoss();
            if(SwapingEvent != null) {
                SwapingEvent();
            }
        }
    }

    void UpdateHpText() {
        hp1.text = "BHP: " + current_health;
        hp2.text = "BHP: " + current_health;
    }
}
