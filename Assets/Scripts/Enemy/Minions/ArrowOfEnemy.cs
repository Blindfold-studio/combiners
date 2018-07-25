using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOfEnemy : Minions, IFPoolObject
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

    public void ObjectSpawn()
    {
        TargetPlayer = FindTheClosestPlayer();
        rb2d = GetComponent<Rigidbody2D>();
        dir = Vector3.Normalize(TargetPlayer.transform.position - this.transform.position);
        Invoke("Disappear", 10);
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb2d.velocity = dir * speed;
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

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BUllet Get into" + collision);
        if (collision.gameObject.name == "Player2" || collision.gameObject.name == "Player1")
        {
            
                gameObject.SetActive(false);
        }
    }
    

}

