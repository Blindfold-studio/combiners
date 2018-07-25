using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Minions
{
    Rigidbody2D rg2d;
    [SerializeField]
    private float speed;
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

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        TargetPlayer = FindTheClosestPlayer();
        heal = 1;
    }

    void FixedUpdate()
    {
        Movement();
        Dead();
    }

    void Movement()
    {

        transform.position = Vector3.SmoothDamp(transform.position, TargetPlayer.transform.position, ref smoothVector3, timeReach);
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
        }
    }

    public void Dead()
    {
        if (heal == 0)
        {
            Destroy(gameObject);
            DropItem(this.transform);
        }
    }
}
