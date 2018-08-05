using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(BoxCollider2D))]
public class Boss3LongRangeAttack : MonoBehaviour {

	private Boss3Movement boss3Movement;
    private bool isPlayerInRange;
    private GameObject straightAxe;
    private GameObject projectileAxe;
    [SerializeField]
    private float attackDurationTime = 2f;

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
	}
	
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
        ProjectileAxeStart();
        yield return new WaitForSeconds(attackDurationTime);
        ProjectileAxeStop(); 
    }

    IEnumerator ThrowStraightAxeToPlayer() {
        StraightAxeStart();
        yield return new WaitForSeconds(attackDurationTime);
        StraightAxeStop();
    }

    void ProjectileAxeStart ()
    {
        projectileAxe.transform.position = this.transform.position;
        boss3Movement.CurrentState = Boss3Movement.State.IsLongRangeAttacking;
        boss3Movement.SetActiveShield(false);
        Debug.Log("Start throwing an axe in a projectile line.");
        projectileAxe.SetActive(true);
        projectileAxe.GetComponent<ProjectileAxeBehavior>().Moving(boss3Movement.TargetPlayer);
    }

    void ProjectileAxeStop ()
    {
        boss3Movement.SetActiveShield(true);
        projectileAxe.SetActive(false);
        projectileAxe.transform.position = this.transform.position;
        Debug.Log("Stop throwing an axe in a straight line.");
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
    }

    void StraightAxeStart ()
    {
        straightAxe.transform.position = this.transform.position;
        boss3Movement.CurrentState = Boss3Movement.State.IsLongRangeAttacking;
        boss3Movement.SetActiveShield(false);
        Debug.Log("Start throwing an axe in a straight line.");
        straightAxe.transform.position = new Vector2(straightAxe.transform.position.x, boss3Movement.TargetPlayer.transform.position.y);
        straightAxe.SetActive(true);
        straightAxe.GetComponent<StraightAxeBehavior>().Moving(boss3Movement.TargetPlayer);
    }

    void StraightAxeStop ()
    {
        boss3Movement.SetActiveShield(true);
        straightAxe.SetActive(false);
        straightAxe.transform.position = this.transform.position;
        Debug.Log("Stop throwing an axe in a straight line.");
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
        Debug.Log(boss3Movement.CurrentState);
    }

    private void StopAttack() {
        isPlayerInRange = false;
        StopCoroutine("ThrowStraightAxeToPlayer");
        StopCoroutine("ThrowProjectileAxeToPlayer");
        straightAxe.SetActive(false);
        projectileAxe.SetActive(false);
    }

    private void OnEnable()
    {
        Boss3Movement.StopCoroutineEvent += StopAttack;
    }

    private void OnDisable()
    {
        Boss3Movement.StopCoroutineEvent -= StopAttack;
    }
}
