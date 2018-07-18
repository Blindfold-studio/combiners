using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D))]
public class Boss3MiddleRangeAttack : MonoBehaviour {

	private Boss3Movement boss3Movement;
    private Rigidbody2D rb;
    private bool isPlayerInRange;
    [SerializeField]
    private float beforeChargingTime = 1f;
    [SerializeField]
    private float afterChargingTime = 1f;
    [SerializeField]
    private float chargedPower = 7f;

	// Use this for initialization
	void Start () {
		boss3Movement = GetComponentInParent<Boss3Movement>();
        rb = GetComponentInParent<Rigidbody2D>();
        isPlayerInRange = false;
	}
	
	void FixedUpdate () {
		if(isPlayerInRange && boss3Movement.CurrentState == Boss3Movement.State.Moving) {
            StartCoroutine("ChargingToPlayer");
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
        Boss3Movement.StopCoroutineEvent += StopAttack;
        boss3Movement.CurrentState = Boss3Movement.State.IsMiddleRangeAttacking;  
        Debug.Log("Start charging!");
        float vel = rb.velocity.x;
        rb.velocity = new Vector2(0, rb.velocity.y);
        Debug.Log("On the way");
        yield return new WaitForSeconds(beforeChargingTime);
        rb.velocity = new Vector2(vel*chargedPower, rb.velocity.y);
        Debug.Log("Charging attack stops!");
        yield return new WaitForSeconds(afterChargingTime);
        rb.velocity = new Vector2(vel, rb.velocity.y);
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
        Boss3Movement.StopCoroutineEvent -= StopAttack;
    }

    void StopAttack() {
        isPlayerInRange = false;
        StopCoroutine("ChargingToPlayer");
        rb.velocity = new Vector2(0, rb.velocity.y);
        Boss3Movement.StopCoroutineEvent -= StopAttack;
    }
}
