using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Boss3Movement : MonoBehaviour {

	private GameObject targetPlayer;
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float stuntAfterPlayerJumpOverHead = 0.6f;
    [SerializeField]
    private GameObject shield;
    
    private bool isFacingRight;
    //this variable will determine if a player go to the other side of the boss, the boss will be stunt for 0.5s
    private bool onHoldForPlayerJump;
    private float distanceToCamera;
    private float screenPadding = 0.5f;
    private float xMin;
    private float xMax;

    public enum State {Idle, Moving, IsShortRangeAttacking, IsMiddleRangeAttacking, IsLongRangeAttacking};
    private State state;

    private Rigidbody2D rg;
    private MissionManager missionManager;
    private Transform player1_screen;
    private Transform player2_screen;

    public static event Action StopCoroutineEvent;

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

	void Start () {
        missionManager = MissionManager.instance;

		rg = GetComponent<Rigidbody2D>();
        onHoldForPlayerJump = false;
        state = State.Moving;
        targetPlayer = FindTheClosestPlayer();
        BossHealth.SwapingEvent += SwapBoss;
        BossHealth.DeathEvent += Die;
        FlipCharacter(targetPlayer.transform.position.x - this.transform.position.x);

        SetPositionNotOverViewPort();
    }

    private void Update() {
        StartCoroutine(FlipCharacter(targetPlayer.transform.position.x - this.transform.position.x));
        TargetPlayer = FindTheClosestPlayer();
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, transform.position.z);
    }
    private void FixedUpdate() {
        if(!onHoldForPlayerJump && state == State.Moving) {
            MoveTowardPlayer();
        } else if(state != State.Moving && state != State.IsMiddleRangeAttacking) {
            rg.velocity = new Vector2(0f, rg.velocity.y);   
        }
    }

    public GameObject TargetPlayer
    {
        get
        {
            return targetPlayer;
        }

        set
        {
            targetPlayer = value;
        }
    }

    public GameObject FindTheClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        GameObject targetPlayer = null;
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Vector2.Distance(this.transform.position, players[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetPlayer = players[i];
            }
        }
        return targetPlayer;
    }

    public void SetActiveShield (bool value)
    {
        shield.SetActive(value);
    }

    IEnumerator FlipCharacter(float horizontalMovement) {
        if((isFacingRight && horizontalMovement < 0) || 
           (!isFacingRight && horizontalMovement > 0)) {
            onHoldForPlayerJump = true;
            isFacingRight = !isFacingRight;
            yield return new WaitForSeconds(stuntAfterPlayerJumpOverHead);
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

    void SetPositionNotOverViewPort()
    {
        distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftmost.x + screenPadding;
        xMax = rightmost.x - screenPadding;
        Debug.Log("x min: " + xMin + "x max: " + xMax);
    }

    void SwapBoss() {
        Debug.Log("Swaping");
        if(StopCoroutineEvent != null) {
            StopCoroutineEvent();
        }
        if(TargetPlayer.name == "Player1") {
            this.transform.position = missionManager.GetBossPosition_P2();
        } else if(TargetPlayer.name == "Player2") {
            this.transform.position = missionManager.GetBossPosition_P1();
        }
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Moving;
    }

    void Die() {
        Debug.Log("Die");
        if(StopCoroutineEvent != null) {
            StopCoroutineEvent();
        }
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
