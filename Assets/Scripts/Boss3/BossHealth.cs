using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class BossHealth : MonoBehaviour {

    
    public TextMeshProUGUI hp1;
    public TextMeshProUGUI hp2;
    [SerializeField]
    public float maxHealth;
    [SerializeField]
    public float numberOfTimeBossSwap;
    [SerializeField]
    private float colliderDisableTime;
    [SerializeField]
    private float waitTime;

    public static float currentHealth;
    private bool isSwapTime;
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
        isSwapTime = false;  
		currentHealth = maxHealth;
        bossCollider = GetComponent<BoxCollider2D>();
        UpdateHpText();
	}

    void CheckingBossHealth() {
        isSwapTime = false;
        UpdateHpText();
        StartCoroutine("ProtectionAfterReceivedAnAttack");
        if (currentHealth <= 0) {
            if(DeathEvent != null) {
                DeathEvent();
            }
        } else if(currentHealth % (maxHealth/numberOfTimeBossSwap) == 0 && currentHealth < maxHealth) {
            isSwapTime = true;
            // SwapBoss();
            if(SwapingEvent != null) {
                bossCollider.enabled = false;
                SwapingEvent();
                bossCollider.enabled = true;
            }
        }
    }

    void UpdateHpText() {
        hp1.text = "BHP: " + currentHealth;
        hp2.text = "BHP: " + currentHealth;
    }

    IEnumerator ProtectionAfterReceivedAnAttack() {
        bossCollider.enabled = false;
        GetComponent<Animation>().Play("BossGetDamage");
        yield return new WaitForSeconds(colliderDisableTime);
        if (!isSwapTime)
            bossCollider.enabled = true;
        GetComponent<Animation>().Stop("BossGetDamage");
        yield return null;
    }
}
