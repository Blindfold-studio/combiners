using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthPotion : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            Debug.Log("current health: " + HealthSystem.instance.HP);
            HealthSystem.instance.HP = 1;
            this.gameObject.SetActive(false);
        }
    }

}
