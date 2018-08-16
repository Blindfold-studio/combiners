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
    bool facingR;
    bool isAlive;
    private float distance;
    private GameObject targetPlayer;
    public GameObject arrow;
    ProjectilePool pool;
    Animator animation;

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
        reloadShot = startShotReload;
        isAlive = true;
        facingR = true;
        TargetPlayer = FindTheClosestPlayer();
        StartCoroutine(Flip());
        pool = ProjectilePool.Instance;
        heal = 1;
        animation = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        EnemyShot();
        Movement();
        Dead();
        distance = targetPlayer.transform.position.x - transform.position.x;
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

    
    IEnumerator Flip()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(.3f);

            if (facingR && (distance > 0))
            {

                Vector3 Scale = transform.localScale;
                if (Scale.x < 0)
                {
                    Scale.x *= -1;
                }
                transform.localScale = Scale;
                facingR = false;
            }

            else if (!facingR && (distance < 0) || facingR && (distance < 0))
            {

                Vector3 Scale = transform.localScale;
                if (Scale.x > 0)
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

    void EnemyShot()
    {
        if (reloadShot <= 0)
        {
            //animation.SetBool("isShoot", true);
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
            //animation.SetBool("isShoot", false);
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
            isAlive = false;
        }
    }
    

}
