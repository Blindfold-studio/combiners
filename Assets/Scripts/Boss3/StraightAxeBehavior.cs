using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAxeBehavior : MonoBehaviour {

    private Rigidbody2D rb;
    [SerializeField]
    private float axeSpeed = 10f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            this.gameObject.SetActive(false);
        }
    }

	public void Moving(GameObject targetPlayer) {
        if(targetPlayer.transform.position.x - transform.position.x < 0) {
            Debug.Log(rb);
            rb.velocity = new Vector2(-1*axeSpeed, rb.velocity.y);
        } else {
            rb.velocity = new Vector2(axeSpeed, rb.velocity.y);
        }
    }

}
