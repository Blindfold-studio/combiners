using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossFlyingMovement : Boss {

    [SerializeField]
    private float rangeX = 10.0f;
    [SerializeField]
    private float rangeY = 1.0f;
    [SerializeField]
    private float speedX = 1.25f;
    [SerializeField]
    private float speedY = 2.5f;
    public float initiatePoint;
    private float x, y;
    private int count = 0;
    private float offSet;
    public Vector3 curPosition;

    public enum State { Idle, Moving, MoveCircle};
    private State state;
    private MissionManager missionManager;
    private Transform player1_screen;
    private Transform player2_screen;
    public static event Action StopCoroutineEvent;

    GameObject minion, minion2;
    SpawnEnemyFly minionFly;
    SpawnEnemyOnGround minionOnGround;

    private GameObject targetPlayer;

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

    void Start ()
    {
        missionManager = MissionManager.instance;
        offSet = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;
        curPosition = this.transform.position;
        state = State.Moving;
    }

    void Update()
    {
       
        TargetPlayer = FindTheClosestPlayer();
        if(state == State.Moving)
        {
            Controll();
        }
        
        
    }

    void Controll()
    {

        if (count % 2 == 0)
        {
            initiatePoint -= Time.deltaTime;
        }
        else if (count % 2 == 1)
        {
            initiatePoint += Time.deltaTime;
        }
        x = rangeX * Mathf.Cos(speedX * initiatePoint);
        y = rangeY * Mathf.Sin(speedY * initiatePoint) + offSet;
        transform.localPosition = new Vector3(x, y, 0);

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
            offSet = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;
            
        }
        else if (TargetPlayer.name == "Player2")
        {
          
            this.transform.position = missionManager.GetBossPosition_P1();
            offSet = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;
            
        }
        curPosition = this.transform.position;
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Moving;
        initiatePoint = 0;

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
