using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAtk : MonoBehaviour {
    [SerializeField]
    private float reloadShot;
    [SerializeField]
    private float startShot;

    private int numOfProjectile;
    private int rangeOfProjectile;
    private float speedbullet;
    
    private GameObject player;
    ProjectilePool pool;

    void Start () {
        reloadShot = startShot;
        pool = ProjectilePool.Instance;
        player = GameObject.FindGameObjectWithTag("Player");
	}

    void FixedUpdate()
    {
        EnemyShot();
        
    }

    //3Projectile----
    void FireThreeProjectile()
    {
        numOfProjectile = 3;
        rangeOfProjectile = 5;
        speedbullet = 3;

        SetProjectile(numOfProjectile, rangeOfProjectile, speedbullet);
    }

    //8Projectile
    void FireEightProjectile()
    {
        numOfProjectile = 8;
        rangeOfProjectile = 30;
        speedbullet = 3;

        SetProjectile(numOfProjectile, rangeOfProjectile, speedbullet);
    }

    //Manyprojectile
    void SetProjectile(int numOfP, float range, float speedBullet)
    {
        float angleTotal = 360f / numOfP;
        float angle = 0f;

        for (int i = 0; i <= numOfP - 1; i++)
        {
            float projectileX = player.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * range;
            float projectileY = player.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * range;

            Vector3 projectileVec = new Vector3(projectileX, projectileY, 0);
            Vector3 projectileDir = Vector3.Normalize(projectileVec - this.transform.position) * speedBullet;

            
            var proj = pool.GetElementInPool("Boss-Many", transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);
           

            angle += angleTotal;

        }
    }

    void EnemyShot()
    {
        if (reloadShot <= 0)
        {
           
            var number = Random.Range(0, 4);

            if (number == 0)
            {
                pool.GetElementInPool("Boss-straight", transform.position, Quaternion.identity);
            }
            else if (number == 1)
            {
                pool.GetElementInPool("Boss-follow", transform.position, Quaternion.identity);
            }
            else if (number == 2)
            {
                FireThreeProjectile();
            }
            else if (number == 3)
            {
                FireEightProjectile();
            }
            else if (number == 4)
            {             
            }

            reloadShot = startShot;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }

    }
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }   

}
