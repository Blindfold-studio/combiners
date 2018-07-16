using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Boss3Movement : MonoBehaviour {

	private GameObject targetPlayer;
    public float speed = 2f;
    private Rigidbody2D rg;
    private bool isFacingRight;

    //this variable will determine if a player go to the other side of the boss, the boss will be stunt for 0.5s
    private bool onHoldForPlayerJump;

    public enum State {Idle, Moving, IsShortRangeAttacking, IsMiddleRangeAttacking, IsLongRangeAttacking};
    private State state;

    /*
    public GameObject player1_screen;
    public GameObject player2_screen;
    */

    private MissionManager missionManager;
    private Transform player1_screen;
    private Transform player2_screen;

    public State CurrentState {
        get { return state; } 
        set {
            if(value == State.Moving) {
                state = value;
            } else if(state == State.Moving) {
                state = value;
            }
        }
    }

    public GameObject TargetPlayer {
        get {
            return targetPlayer;
        }

        set {
            targetPlayer = value;
        }
    }

	// Use this for initialization
	void Start () {
        missionManager = MissionManager.instance;

		rg = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        onHoldForPlayerJump = false;
        state = State.Moving;
        targetPlayer = FindTheClosestPlayer();
        BossHealth.SwapingEvent += SwapBoss;
        BossHealth.DeathEvent += Die;
        player1_screen = missionManager.GetBossPosition_P1();
        player2_screen = missionManager.GetBossPosition_P2();
	}

    private void Update() {
        StartCoroutine(FlipCharacter(targetPlayer.transform.position.x - this.transform.position.x));
        
    }
    private void FixedUpdate() {
        if(!onHoldForPlayerJump && state == State.Moving) {
            MoveTowardPlayer();
        } else if(state != State.Moving && state != State.IsMiddleRangeAttacking) {
            rg.velocity = new Vector2(0f, rg.velocity.y);   
        }
    }

    IEnumerator FlipCharacter(float horizontalMovement) {
        if((isFacingRight && horizontalMovement < 0) || 
           (!isFacingRight && horizontalMovement > 0)) {
            onHoldForPlayerJump = true;
            isFacingRight = !isFacingRight;
            yield return new WaitForSeconds(0.5f);
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            onHoldForPlayerJump = false;
        }
    }

    void MoveTowardPlayer() {
        float vel = speed;
        if(!isFacingRight && speed > 0) {
            vel *= -1;
        } else if(isFacingRight && speed < 0) {
            vel *= -1;
        }
        rg.velocity = new Vector2(vel, rg.velocity.y);
    }

    public GameObject FindTheClosestPlayer() {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        GameObject targetPlayer = null;
        for(int i = 0 ; i < players.Length ; i++) {
            float distance = Vector2.Distance(this.transform.position, players[i].transform.position);
            if(distance < minDistance) {
                minDistance = distance;
                targetPlayer = players[i];
            }
        }
        return targetPlayer;
    }

    IEnumerator SwapBoss() {
        Debug.Log("Swaping");

        if(CurrentState == State.Moving) {
            // CurrentState = State.Idle;
        } else if(CurrentState != State.Idle && CurrentState != State.Moving) {
            yield return new WaitUntil(() => CurrentState == State.Moving);
        }

        if(TargetPlayer.name == "Player1") {
            //this.transform.position = player2_screen.transform.position;
            this.transform.position = player2_screen.position;
        } else if(TargetPlayer.name == "Player2") {
            //this.transform.position = player1_screen.transform.position;
            this.transform.position = player1_screen.position;
        }
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Moving;
    }

    void Die() {
        Debug.Log("Die");
        CurrentState = State.Idle;
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
        rg.velocity = new Vector2(0f, rg.velocity.y);   
    }

    private void OnDisable() {
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
    }
}
