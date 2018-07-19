using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthPotion : MonoBehaviour {

    private HealthSystem healthSystem;

    void Start ()
    {
        healthSystem = HealthSystem.instance;
    }

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            healthSystem = HealthSystem.instance;
            Debug.Log("current health: " + healthSystem.HP);
            healthSystem.HP = 1;
            this.gameObject.SetActive(false);
        }
    }

}
