using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WizardMovement : Boss {


    public enum State { Idle, Moving};
    private State state;
    private MissionManager missionManager;
    private Transform player1_screen;
    private Transform player2_screen;
    private GameObject targetPlayer;
    public static event Action StopCoroutineEvent;

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

    void Start () {
        missionManager = MissionManager.instance;

    }
	
	
	void Update () {
        TargetPlayer = FindTheClosestPlayer();
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
        TargetPlayer = FindTheClosestPlayer();
        CurrentState = State.Moving;
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
