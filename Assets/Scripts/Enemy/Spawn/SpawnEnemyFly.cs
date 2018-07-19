﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFly : Minions {

    public static SpawnEnemyFly instance = null;

    void Awake()
    {
        instance = this;
    }
    [SerializeField]
    private List<Transform> minionPosition_P1;
    [SerializeField]
    private List<Transform> minionPosition_P2;

    public GameObject minion;
    private float x;
    [SerializeField]
    private float spawnTimer;
    private float spawn;
    Vector2 locate;
	// Use this for initialization

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (upSide)
        {
            
            if (Time.time > spawn)
            {
                Debug.Log("Player1 fly");
                spawn = spawnTimer + Time.time;
                x = Random.Range(transform.position.x - 14, transform.position.x + 14);
                locate = new Vector2(x, minionPosition_P1[0].position.y);
                Instantiate(minion, locate, Quaternion.identity);
               
            }
        }
        else
        {
            
            if (Time.time > spawn)
            {
                Debug.Log("Player2 fly");
                spawn = spawnTimer + Time.time;
                x = Random.Range(transform.position.x - 14, transform.position.x + 14);
                locate = new Vector2(x, minionPosition_P2[0].position.y);
                Instantiate(minion, locate, Quaternion.identity);
                
            }
        }
    }

    private void OnEnable()
    {
        BossHealth.SwapingEvent += SetSide;
    }

    private void OnDisable()
    {
        BossHealth.SwapingEvent -= SetSide;
    }

    public bool UpSide
    {
        get
        {
            return upSide;
        }

        set
        {
            upSide = value;
        }
    }

    public void SetSide()
    {
        
        if (upSide)
        {
            upSide = false;
        }
        else
        {
            upSide = true;
        }
        Debug.Log("Chec Set Side Flying" + upSide);
    }

    

    public Transform GetMinionPosition_P1()
    {
        return minionPosition_P1[0];
    }

    public Transform GetMinionPosition_P2()
    {
        return minionPosition_P2[0];
    }
}
