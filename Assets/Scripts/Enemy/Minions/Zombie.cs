using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Minions, IFPoolObject {
    Rigidbody2D rg2d;
    [SerializeField]
    private float speed;
    public float timeReach;
    private Vector3 smoothVector3 = Vector3.zero;
    bool facingR;
    private float distance;
    private GameObject targetPlayer;
    bool isAlive;
    
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
        rg2d = GetComponent<Rigidbody2D>();
        isAlive = true;
        facingR = true;
        TargetPlayer = FindTheClosestPlayer();
        StartCoroutine(Flip());
        heal = 1;
    }

    void FixedUpdate()
    {
        Movement();
        distance = targetPlayer.transform.position.x - transform.position.x;
        Dead();
    }

    void Movement()
    {
        transform.position = Vector3.SmoothDamp(transform.position, TargetPlayer.transform.position, ref smoothVector3, timeReach);
    }

    IEnumerator Flip()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(.3f);

            if (facingR && (distance > 0))
            {
                
                Vector3 Scale = transform.localScale;
                if (Scale.x > 0)
                {
                    Scale.x *= -1;
                }
                transform.localScale = Scale;
                facingR = false;
            }
            
            else if (!facingR && (distance < 0) || facingR && (distance < 0))
            {
                Vector3 Scale = transform.localScale;
                if (Scale.x < 0)
                {
                    Scale.x *= -1;
                }
                transform.localScale = Scale;
                facingR = true;
            }
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("NoneEffectOnPlayer"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision, true);

        }
        else if (collision.CompareTag("Weapon"))
        {
            TakeDamage();
            isAlive = false;
        }
    }
}
