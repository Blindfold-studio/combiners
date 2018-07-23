﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOfEnemy : Minions
{

    Rigidbody2D rb2d;

    [SerializeField]
    private float speed;

    EnemyFlip flip;
    private Vector2 dir;

    private Transform player;
    private GameObject targetPlayer;

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
    // Use this for initialization
    void Start()
    {

        TargetPlayer = FindTheClosestPlayer();
        rb2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Dir();
    }

    void Dir()
    {
        if (targetPlayer.transform.position.x > this.transform.position.x)
        {
            rb2d.velocity = new Vector2(speed*Time.deltaTime * (-1), 0);
        }
        else
        {
            rb2d.velocity = new Vector2(speed * Time.deltaTime, 0);
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


    void OnBecameInvisible()
    {
        gameObject.SetActive(false); 
    }
}
