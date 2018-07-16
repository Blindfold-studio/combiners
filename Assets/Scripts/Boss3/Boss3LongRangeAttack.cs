using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D))]
public class Boss3LongRangeAttack : MonoBehaviour {

	private Boss3Movement boss3Movement;
    private bool isPlayerInRange;
    private GameObject straightAxe;
    private GameObject projectileAxe;

	// Use this for initialization
	void Start () {
		isPlayerInRange = false;
        boss3Movement = GetComponentInParent<Boss3Movement>();
        for(int i = 0 ; i < transform.childCount ; i++) 
        {
            if(transform.GetChild(i).gameObject.name == "StraightAxe") {
                straightAxe = transform.GetChild(i).gameObject;
            } else if(transform.GetChild(i).gameObject.name == "ProjectileAxe") {
                projectileAxe = transform.GetChild(i).gameObject;
            }
        }

        Boss3Movement.StopCoroutineEvent += StopAttack;
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerInRange && boss3Movement.CurrentState == Boss3Movement.State.Moving) {
            
            if(boss3Movement.TargetPlayer.transform.position.y - transform.position.y < 0) {
                StartCoroutine("ThrowStraightAxeToPlayer");  
            } else {
                StartCoroutine("ThrowProjectileAxeToPlayer");  
            }
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

    IEnumerator ThrowProjectileAxeToPlayer() {
        boss3Movement.CurrentState = Boss3Movement.State.IsLongRangeAttacking;
        Debug.Log("Start throwing an axe in a projectile line.");
        projectileAxe.SetActive(true);
        projectileAxe.GetComponent<ProjectileAxeBehavior>().Moving(boss3Movement.TargetPlayer);
        yield return new WaitForSeconds(2f);
        projectileAxe.SetActive(false);
        projectileAxe.transform.position = this.transform.position;
        Debug.Log("Stop throwing an axe in a straight line.");
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
    }

    IEnumerator ThrowStraightAxeToPlayer() {
        boss3Movement.CurrentState = Boss3Movement.State.IsLongRangeAttacking;  
        Debug.Log("Start throwing an axe in a straight line.");
        straightAxe.transform.position = new Vector2(straightAxe.transform.position.x, boss3Movement.TargetPlayer.transform.position.y);
        straightAxe.SetActive(true);
        straightAxe.GetComponent<StraightAxeBehavior>().Moving(boss3Movement.TargetPlayer);
        yield return new WaitForSeconds(2f);
        straightAxe.SetActive(false);
        straightAxe.transform.position = this.transform.position;
        Debug.Log("Stop throwing an axe in a straight line.");
        // yield return new WaitForSeconds(1f);
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
        Debug.Log(boss3Movement.CurrentState);
    }


    private void StopAttack() {
        StopCoroutine("ThrowStraightAxeToPlayer");
        StopCoroutine("ThrowProjectileAxeToPlayer");
        straightAxe.SetActive(false);
        projectileAxe.SetActive(false);
        Boss3Movement.StopCoroutineEvent -= StopAttack;
    }
}
