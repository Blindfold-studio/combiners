using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Minions
{

    //Rigidbody2D rb2d;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float stop;
    [SerializeField]
    private float retreat;
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
        
    }

    void FixedUpdate()
    {
        EnemyShot();
        Movement();
    }

    void Movement()
    {
        if(Vector2.Distance(targetPlayer.transform.position, transform.position) > stop)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, speed * Time.deltaTime);
        }
        else if(Vector2.Distance(targetPlayer.transform.position, transform.position) < stop && Vector2.Distance(targetPlayer.transform.position, this.transform.position) > retreat)
        {
            transform.position = this.transform.position;
        }
        else if(Vector2.Distance(targetPlayer.transform.position, transform.position) < retreat)
        {
            Debug.Log("Retreat");
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, -speed * Time.deltaTime);
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
