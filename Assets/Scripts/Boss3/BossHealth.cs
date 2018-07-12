using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

    public Image bossHpBar;
    private float max_health = 39f;
    private float current_health;
    private Boss3Movement boss3Movement;

    public static event Func<IEnumerator> SwapingEvent;
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
	}
	
	void CheckingBossHealth() {
        if(current_health <= 0) {
            if(DeathEvent != null) {
                DeathEvent();
            }
        } else if(current_health % 13 == 0 && current_health < max_health) {    
            // SwapBoss();
            if(SwapingEvent != null) {
                StartCoroutine(SwapingEvent());
            }
        }
    }
}
