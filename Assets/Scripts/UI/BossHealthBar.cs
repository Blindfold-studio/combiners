using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {

    public Image bossHpBar;
    BossHealth bossHealth;

    // Use this for initialization
    void Start () {
        bossHpBar = GetComponent<Image>();
        bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
	}
	
	// Update is called once per frame
	void Update () {
        bossHpBar.fillAmount = BossHealth.currentHealth / bossHealth.maxHealth;
	}
}
