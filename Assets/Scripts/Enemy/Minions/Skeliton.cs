﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeliton : Minions, IFPoolObject
{
    Rigidbody2D rg2d;
    EnemyFlip flip;
    [SerializeField]
    private float speed;
    GameObject skel;
   
    public float timeReach;
    private Vector3 smoothVector3 = Vector3.zero;
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
    public void ObjectSpawn() {
        // flip = GameObject.Find("SkelitonGuy").GetComponent<EnemyFlip>();
        
        rg2d = GetComponent<Rigidbody2D>();
        TargetPlayer = FindTheClosestPlayer();
        heal = 1;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Movement();
        Dead();
	}

    void Movement()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, TargetPlayer.transform.position, ref smoothVector3, timeReach);
    }
    /* void Movement()
     {
         float vel = speed;   
         if (flip.facingR)
         {
             vel *= 1;
         }
         else if(!flip.facingR)
         {
             vel *= -1;
         }
         rg2d.velocity = new Vector2(vel, rg2d.velocity.y);

     }*/
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("NoneEffectOnPlayer"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(),collision,true);

        }
        else if (collision.CompareTag("Weapon"))
        {
            TakeDamage();
        }
    }
}
