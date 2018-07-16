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
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerInRange && boss3Movement.CurrentState == Boss3Movement.State.Moving && targetPlayer != null) {
            
            if(targetPlayer.transform.position.y - transform.position.y < 0) {
                boss3Movement.recentCoroutine = StartCoroutine(ThrowStraightAxeToPlayer());  
            } else {
                boss3Movement.recentCoroutine = StartCoroutine(ThrowProjectileAxeToPlayer());
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

        projectileAxe.GetComponent<ProjectileAxeBehavior>().Moving(targetPlayer);
        
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
        straightAxe.GetComponent<StraightAxeBehavior>().Moving(targetPlayer);
        yield return new WaitForSeconds(2f);
        straightAxe.SetActive(false);
        straightAxe.transform.position = this.transform.position;
        Debug.Log("Stop throwing an axe in a straight line.");
        // yield return new WaitForSeconds(1f);
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
    }
}
