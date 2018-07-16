using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Boss3ShortRangeAttack : MonoBehaviour {

	private bool isPlayerInRange;
    // private GameObject targetPlayer;
    private Boss3Movement boss3Movement;

    private void Start() {
        isPlayerInRange = false;
        // targetPlayer = null;
        
        boss3Movement = GetComponentInParent<Boss3Movement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = true;
            // targetPlayer = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) {
            isPlayerInRange = false;
            // targetPlayer = null;
        }
    }

    private void Update() {
        
        if(isPlayerInRange && boss3Movement.CurrentState == Boss3Movement.State.Moving /*&& targetPlayer != null*/) {
            StartCoroutine(SlashPlayer(boss3Movement.TargetPlayer));
        }
    }

    IEnumerator SlashPlayer(GameObject player) {
        //do something
        boss3Movement.CurrentState = Boss3Movement.State.IsShortRangeAttacking;
        Debug.Log(player.name + " was attacked by sword!");
        print(boss3Movement.CurrentState);
        yield return new WaitForSeconds(5f);
        boss3Movement.CurrentState = Boss3Movement.State.Moving;
        print(boss3Movement.CurrentState);
        Debug.Log("The sword attack stops!");
    }
}
