using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WizardMovement : MonoBehaviour {

    public enum State { Idle, Moving};
    private State state;
    private MissionManager missionManager;
    private GameObject targetPlayer;
    private Transform player1_screen;
    private Transform player2_screen;
    public static event Action StopCoroutineEvent;

    public bool inPlayer1;
    private int currentPosition;
    [SerializeField]
    private float teleportTimeDelay;
    private float teleportTime;

    public Vector3 curPosition;
    public GameObject portalPoint;


    public State CurrentState
    {
        get { return state; }
        set
        {
            if (value == State.Moving)
            {
                state = value;
            }
            else
            {
                state = value;
            }
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

    void Awake()
    {
        BossHealth.SwapingEvent += SwapBoss;
        BossHealth.DeathEvent += Die;
    }

    void Start()
    {
        missionManager = MissionManager.instance;
        currentPosition = 0;
        state = State.Idle;
        StartCoroutine("DelayBossSpawn");
    }

    IEnumerator DelayBossSpawn()
    {
        yield return new WaitForSeconds(0);
        curPosition = this.transform.position;
        CheckBossPosition();
        state = State.Moving;
    }

    void Update()
    {
        TargetPlayer = FindTheClosestPlayer();
        teleportTime -= Time.deltaTime;

        if(state == State.Moving)
        {
            Movement();
        }
        
    }

    void Movement()
    {
        if (inPlayer1)
        {
            portalPoint.transform.position = new Vector3(0f, 50f, 0f);
        }
        else
        {
            portalPoint.transform.position = new Vector3(0f, 0f, 0f);
        }

       
        if(teleportTime <= 0)
        {
            teleportTime = teleportTimeDelay;
            currentPosition++;
        }

        if (currentPosition == portalPoint.transform.childCount )
        {
            currentPosition = 0;
        }

        transform.position = portalPoint.transform.GetChild(currentPosition).position;
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

    void SwapBoss()
    {
        if (StopCoroutineEvent != null)
        {
            StopCoroutineEvent();
        }
        if (TargetPlayer.name == "Player1")
        {

            this.transform.position = missionManager.GetBossPosition_P2();

        }
        else if (TargetPlayer.name == "Player2")
        {

            this.transform.position = missionManager.GetBossPosition_P1();      
        }
        CheckBossPosition();
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Idle;
    }

    void CheckBossPosition()
    {
        if (curPosition.y >= 25)
        {
            inPlayer1 = true;
        }
        else
        {
            inPlayer1 = false;
        }
    }

    void Die()
    {
        CurrentState = State.Idle;
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
    }

    private void OnDisable()
    {
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
    }

}
