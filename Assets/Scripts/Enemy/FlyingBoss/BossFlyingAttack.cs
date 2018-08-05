using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyingAttack : MonoBehaviour {

    [SerializeField]
    private float reloadShot;
    [SerializeField]
    private float startShot;

    GameObject objectPool;
    private GameObject player;
    ProjectilePool pool;
    private int numOfProjectile;
    private int rangeOfProjectile;
    private float speedbullet;
    Vector3 projectileVec;
    Vector3 projectileDir;
    GameObject bullet;
    BossFlyingMovement Bossmovement;

    void Start()
    {
        reloadShot = startShot;
        pool = ProjectilePool.Instance;
        
    }

    void FixedUpdate()
    {
        player = FindTheClosestPlayer();
        EnemyShot();
    }
    void EnemyShot()
    {
        if (reloadShot <= 0)
        {
            var number = Random.Range(0, 4);

            if (number == 0)
            {
                pool.GetElementInPool("Boss-StraightBullet", transform.position, Quaternion.identity);
            }
            else if (number == 1)
            {
                pool.GetElementInPool("Boss-FollowBullet", transform.position, Quaternion.identity);
            }
            else if (number == 2)
            {
                FireThreeProjectile();
            }
            else if (number == 3)
            {
                FireEightProjectile();
            }
            reloadShot = startShot;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }

    }

    //3Projectile
    public void FireThreeProjectile()
    {

        numOfProjectile = 3;
        rangeOfProjectile = 5;
        speedbullet = 3;

        SetProjectile(numOfProjectile, rangeOfProjectile, speedbullet);
    }

    //8Projectile
    public void FireEightProjectile()
    {
        numOfProjectile = 8;
        rangeOfProjectile = 30;
        speedbullet = 3;

        SetProjectile(numOfProjectile, rangeOfProjectile, speedbullet);
    }

    //Manyprojectile
    public void SetProjectile(int numOfP, float range, float speedBullet)
    {
        float angleTotal = 360f / numOfP;
        float angle = 0f;

        for (int i = 0; i <= numOfP - 1; i++)
        {
            float projectileX = player.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * range;  
            float projectileY = player.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * range;

            projectileVec = new Vector3(projectileX, projectileY, 0);
            projectileDir = Vector3.Normalize(projectileVec - this.transform.position) * speedBullet;

            bullet = pool.GetElementInPool("Boss-SurroundBullet", transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);
       
            angle += angleTotal;

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


}
