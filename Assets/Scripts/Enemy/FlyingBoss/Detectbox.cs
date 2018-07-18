using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Detectbox : Boss {

    [SerializeField]
    private float amplitudeX = 10.0f;
    [SerializeField]
    private float amplitudeY = 1.0f;
    [SerializeField]
    private float omegaX = 1.25f;
    [SerializeField]
    private float omegaY = 2.5f;
    private float index;
    private float x,y;
    private int count = 0;
    private float spawnPoint;

    public enum State { Idle, Moving};
    private State state;
    private MissionManager missionManager;
    private Transform player1_screen;
    private Transform player2_screen;
    public static event Action StopCoroutineEvent;

    GameObject minion,minion2;
    SpawnEnemyFly minionFly,minionSkel;
    
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
            else if (state == State.Moving)
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
        minion = GameObject.Find("SpawnEnemy-Fly");
        minion2 = GameObject.Find("SpawnEnemy-Skel");
        minionFly = minion.GetComponent<SpawnEnemyFly>();
        minionSkel = minion2.GetComponent<SpawnEnemyFly>();
        missionManager = MissionManager.instance;
        targetPlayer = FindTheClosestPlayer();

        BossHealth.SwapingEvent += SwapBoss;
        BossHealth.DeathEvent += Die;
        player1_screen.position = missionManager.GetBossPosition_P1();
        player2_screen.position = missionManager.GetBossPosition_P2();
    }

    void Update () {
        Controll();
    }

    void Controll()
    {
        
        if (count%2==0)
        {
            index -= Time.deltaTime;
        }
        else if(count%2==1)
        {
            index += Time.deltaTime;
        }

        
        x = amplitudeX * Mathf.Cos(omegaX * index);
        /*if (CheckHealh())
        {
            y = amplitudeY * Mathf.Sin(omegaY * index) + 1.5f;
            minionFly.UpSide();
        }
        else
        {
            y = amplitudeY * Mathf.Sin(omegaY * index) + 40.5f;
            minionFly.DownSide();
        }*/
        y = amplitudeY * Mathf.Sin(omegaY * index) + spawnPoint;
        transform.localPosition = new Vector3(x, y, 0);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        
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
        Debug.Log("Swaping");
        if (StopCoroutineEvent != null)
        {
            StopCoroutineEvent();
        }
        if (TargetPlayer.name == "Player1")
        {
            this.transform.position = player2_screen.position;
            spawnPoint = amplitudeY * Mathf.Sin(omegaY * index) + this.transform.position.y;
           
        }
        else if (TargetPlayer.name == "Player2")
        {
            this.transform.position = player1_screen.position;
            spawnPoint = amplitudeY * Mathf.Sin(omegaY * index)+ this.transform.position.y;
            
        }
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Moving;
        minionFly.SetSide();
        minionSkel.SetSide();
        index = 0;
        
    }

    void Die()
    {
        Debug.Log("Die");
        CurrentState = State.Idle;
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
        //rg.velocity = new Vector2(0f, rg.velocity.y);
    }

    private void OnDisable()
    {
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
    }

}
