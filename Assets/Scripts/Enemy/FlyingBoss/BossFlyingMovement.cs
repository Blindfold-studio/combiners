﻿using System.Collections;
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
    private float initiatePoint;
    private float x, y;
    private int count = 0;
    private float offSet;

    public enum State { Idle, Moving };
    private State state;
    private MissionManager missionManager;
    private Transform player1_screen;
    private Transform player2_screen;
    public static event Action StopCoroutineEvent;

    GameObject minion, minion2;
    SpawnEnemyFly minionFly, minionSkel;

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
        
        targetPlayer = FindTheClosestPlayer();

        BossHealth.SwapingEvent += SwapBoss;
        BossHealth.DeathEvent += Die; 
    }

    void Start ()
    {
        missionManager = MissionManager.instance;
    }

    void Update()
    {
        Controll();
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
        Debug.Log("Swaping");
        if (StopCoroutineEvent != null)
        {
            StopCoroutineEvent();
        }
        if (TargetPlayer.name == "Player1")
        {
            player2_screen.position = missionManager.GetBossPosition_P2();
            this.transform.position = player2_screen.position;
            offSet = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;

        }
        else if (TargetPlayer.name == "Player2")
        {
            player1_screen.position = missionManager.GetBossPosition_P1();
            this.transform.position = player1_screen.position;
            offSet = rangeY * Mathf.Sin(speedY * initiatePoint) + this.transform.position.y;

        }
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Moving;
        minionFly.SetSide();
        minionSkel.SetSide();
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
