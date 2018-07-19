using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloorScript : MonoBehaviour {

	private void OnColliderEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy") || other.CompareTag("Boss")
        || other.CompareTag("Player")) {
            Physics2D.IgnoreCollision(other, this.GetComponent<BoxCollider2D>(), true);
        }
    }
}
