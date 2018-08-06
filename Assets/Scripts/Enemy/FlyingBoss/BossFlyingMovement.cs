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
    private float offSetX;
    private float offSetY;
    public Vector3 curPosition;
    public bool inPlayer1;

    public enum State { Idle, Moving, MoveToPlayer, MoveCircle, MoveToCheckBox, HorizontalMove};
    private State state;
    private MissionManager missionManager;
    private BossFlyingAround bossFlyingAround;
    private Transform player1_screen;
    private Transform player2_screen;
    public static event Action StopCoroutineEvent;


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
        bossFlyingAround = GetComponent<BossFlyingAround>();
        state = State.Idle;
        StartCoroutine("SetOffSet");
    }

    IEnumerator SetOffSet()
    {

        yield return new WaitForSeconds(0);
        offSetY = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;
        //offSetX = rangeX * Mathf.Cos(speedX * initiatePoint) + this.transform.position.x;
        curPosition = this.transform.position;
        CheckBossPosition();
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
        y = rangeY * Mathf.Sin(speedY * initiatePoint) + offSetY;
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
            offSetY = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;
            
        }
        else if (TargetPlayer.name == "Player2")
        {
          
            this.transform.position = missionManager.GetBossPosition_P1();
            offSetY = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;
        }
        curPosition = this.transform.position;
        CheckBossPosition();
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Moving;
        initiatePoint = 0;
        bossFlyingAround.RecurrentPosition();

    }

    void CheckBossPosition()
    {
        if(curPosition.y >= 25)
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
