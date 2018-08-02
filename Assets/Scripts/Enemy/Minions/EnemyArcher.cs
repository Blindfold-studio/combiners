using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : Minions, IFPoolObject
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



    public void ObjectSpawn()
    {
        reloadShot = startShotReload;
        TargetPlayer = FindTheClosestPlayer();
        pool = ProjectilePool.Instance;
        heal = 1;
    }

    void FixedUpdate()
    {
        EnemyShot();
        Movement();
        Dead();
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
            TargetPlayer = FindTheClosestPlayer();
            if (targetPlayer.transform.position.x - transform.position.x > 0)
            {
                pool.GetElementInPool("Enemy_Arrow", transform.position, Quaternion.identity);
            }
            else
            {
                pool.GetElementInPool("Enemy_Arrow", transform.position, Quaternion.Euler(new Vector3(0f, 0f, 180f)));
            }
            
            reloadShot = startShotReload;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }
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
    

}
