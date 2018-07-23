using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Minions
{

    //Rigidbody2D rb2d;

    [SerializeField]
    private float speed;

    public float startShotReload;
    private float reloadShot;
    private GameObject targetPlayer;
    public GameObject arrow;
    ProjectilePool pool;

 
    void Start()
    {
        reloadShot = startShotReload;
        TargetPlayer = FindTheClosestPlayer();
        pool = ProjectilePool.Instance;
        Movement();
    }

    void FixedUpdate()
    {
        EnemyShot();
       
    }

    void Movement()
    {
        if (transform.position.x > targetPlayer.transform.position.x)
        {
            Debug.Log("go1");
            transform.position = Vector2.MoveTowards(transform.position, transform.position - new Vector3(3,0,0), speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("go2");
            transform.position = Vector2.MoveTowards(transform.position, transform.position + new Vector3(3, 0, 0), speed * Time.deltaTime);
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

    void EnemyShot()
    {
        if (reloadShot <= 0)
        {
            pool.GetElementInPool("Enemy_Arrow", transform.position, Quaternion.identity);
            reloadShot = startShotReload;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }
    }
}
