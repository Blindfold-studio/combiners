﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour {

    public Image bossHpBar;
    public TextMeshProUGUI hp1;
    public TextMeshProUGUI hp2;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float numberOfTimeBossSwap;
    [SerializeField]
    private float colliderDisableTime;
    private float currentHealth;
    private BoxCollider2D bossCollider;

    public static event Action SwapingEvent;
    public static event Action DeathEvent;

    public float Health {
        get {
            return currentHealth;
        }

        set {
            currentHealth += value;
            CheckingBossHealth();
        }
    }

	// Use this for initialization
	void Start () {
		currentHealth = maxHealth;
        bossCollider = GetComponent<BoxCollider2D>();
        UpdateHpText();
	}
	
	void CheckingBossHealth() {
        UpdateHpText();
        StartCoroutine("ProtectionAfterReceivedAnAttack");
        Debug.Log("Current health test" + currentHealth);
        if (currentHealth <= 0) {
            if(DeathEvent != null) {
                DeathEvent();
            }
        } else if(currentHealth % (maxHealth/numberOfTimeBossSwap) == 0 && currentHealth < maxHealth) {    
            // SwapBoss();
            if(SwapingEvent != null) {
                SwapingEvent();
            }
        }
    }

    void UpdateHpText() {
        hp1.text = "BHP: " + currentHealth;
        hp2.text = "BHP: " + currentHealth;
    }

    IEnumerator ProtectionAfterReceivedAnAttack() {
        bossCollider.enabled = false;
        yield return new WaitForSeconds(colliderDisableTime);
        bossCollider.enabled = true;
    }
}
