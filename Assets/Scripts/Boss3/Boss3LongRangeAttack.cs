using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D))]
public class Boss3LongRangeAttack : MonoBehaviour {

	private Boss3Movement boss3Movement;
    private bool isPlayerInRange;
    private GameObject targetPlayer;
    private GameObject straightAxe;
    private GameObject projectileAxe;

    private Rigidbody2D straightAxeRb;
    private Rigidbody2D projectileAxeRb;
    private float throwingAngle = 45f * Mathf.Deg2Rad;
    private float gravity = 0.8f;


	// Use this for initialization
	void Start () {
		isPlayerInRange = false;
        boss3Movement = GetComponentInParent<Boss3Movement>();
        targetPlayer = null;
        for(int i = 0 ; i < transform.childCount ; i++) 
        {
            if(transform.GetChild(i).gameObject.name == "StraightAxe") {
                straightAxe = transform.GetChild(i).gameObject;
            } else if(transform.GetChild(i).gameObject.name == "ProjectileAxe") {
                projectileAxe = transform.GetChild(i).gameObject;
            }
        }
        Debug.Log(straightAxe);
        straightAxeRb = straightAxe.GetComponent<Rigidbody2D>();
        projectileAxeRb = projectileAxe.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerInRange && boss3Movement.CurrentState == Boss3Movement.State.Moving && targetPlayer != null) {
            
            if(targetPlayer.transform.position.y - transform.position.y < 0) {
                StartCoroutine(ThrowStraightAxeToPlayer());  
            } else {
                StartCoroutine(ThrowProjectileAxeToPlayer());
            }
            
        }
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = true;
            targetPlayer = other.gameObject;
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = false;
            targetPlayer = null;
        }
    }

    IEnumerator ThrowProjectileAxeToPlayer() {
        boss3Movement.CurrentState = Boss3Movement.State.IsLongRangeAttacking;
        //do something
        Debug.Log("Start throwing an axe in a projectile line.");
        projectileAxe.SetActive(true);

        //find time too
        float verticalDistance =  targetPlayer.transform.position.y - this.transform.position.y;
        float horizontalDistance = targetPlayer.transform.position.x - this.transform.position.x;
        float vel = Mathf.Sqrt((gravity*horizontalDistance*horizontalDistance) / (1 + Mathf.Cos(2*throwingAngle)*(horizontalDistance*Mathf.Tan(throwingAngle) - verticalDistance)));
        Debug.Log(verticalDistance);
        Debug.Log(horizontalDistance);
        float vel_x = vel * Mathf.Cos(throwingAngle);
        float vel_y = vel * Mathf.Sin(throwingAngle);
        if(horizontalDistance < 0) {
            vel_x = vel * Mathf.Cos(180f*Mathf.Deg2Rad - throwingAngle);
            vel_y = vel * Mathf.Sin(180f*Mathf.Deg2Rad - throwingAngle);
        } 
        projectileAxeRb.velocity = new Vector2(vel_x, vel_y);
        yield return new WaitForSeconds(2f);
        projectileAxe.SetActive(false);
        projectileAxe.transform.position = this.transform.position;
        Debug.Log("Stop throwing an axe in a straight line.");
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
    }

    IEnumerator ThrowStraightAxeToPlayer() {
        boss3Movement.CurrentState = Boss3Movement.State.IsLongRangeAttacking;  
        //do something
        Debug.Log("Start throwing an axe in a straight line.");
        straightAxe.transform.position = new Vector2(straightAxe.transform.position.x, targetPlayer.transform.position.y);
        straightAxe.SetActive(true);
        if(targetPlayer.transform.position.x - transform.position.x < 0) {
            straightAxeRb.velocity = new Vector2(-10f, straightAxeRb.velocity.y);
        } else {
            straightAxeRb.velocity = new Vector2(10f, straightAxeRb.velocity.y);
        }
        yield return new WaitForSeconds(2f);
        straightAxe.SetActive(false);
        straightAxe.transform.position = this.transform.position;
        Debug.Log("Stop throwing an axe in a straight line.");
        // yield return new WaitForSeconds(1f);
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
    }
}
