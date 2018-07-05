using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthPotion : MonoBehaviour {

    private GameObject gameController;
    private HealthSystem healthSystem;

    private void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController");
        healthSystem = gameController.GetComponent<HealthSystem>();
    }

	private void OnTriggerEnter2D(Collider2D other) {
        healthSystem.HP = 1;
        this.gameObject.SetActive(false);
    }

}
