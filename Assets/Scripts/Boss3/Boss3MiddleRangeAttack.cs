using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D))]
public class Boss3MiddleRangeAttack : MonoBehaviour {

	private Boss3Movement boss3Movement;
    private Rigidbody2D rb;
    private bool isPlayerInRange;

	// Use this for initialization
	void Start () {
		boss3Movement = GetComponentInParent<Boss3Movement>();
        rb = GetComponentInParent<Rigidbody2D>();
        isPlayerInRange = false;
	}
	
	void FixedUpdate () {
		if(isPlayerInRange && boss3Movement.CurrentState == Boss3Movement.State.Moving) {
            StartCoroutine(ChargingToPlayer());
        }
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = false;
        }
    }

    IEnumerator ChargingToPlayer() {
        boss3Movement.CurrentState = Boss3Movement.State.IsMiddleRangeAttacking;  
        Debug.Log("Start charging!");
        float vel = rb.velocity.x;
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(vel*5, rb.velocity.y);
        // rb.AddForce(new Vector2(2000, 0));
        Debug.Log("Charging attack stops!");
        yield return new WaitForSeconds(1.5f);
        rb.velocity = new Vector2(vel, rb.velocity.y);
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
    }
}
