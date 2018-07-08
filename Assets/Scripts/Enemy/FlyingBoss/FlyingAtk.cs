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
    public float fireTime;

    private GameObject player;

    ObjectPoolBullets objectFromPool;
    Projectiles shootProjectiles;

	void Start () {

        //InvokeRepeating("EnemyShot", fireTime, fireTime);
        player = GameObject.FindGameObjectWithTag("Player");
        objectFromPool = ObjectPoolBullets.instance;
        shootProjectiles = Projectiles.instance;
       // shootProjectiles = gameObject.GetComponent<Projectiles>();
        
        
        
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

            
            //var proj = Instantiate(bullet3Pro, this.transform.position, Quaternion.identity);
            //proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);
           

            angle += angleTotal;

        }
    }

    void EnemyShot()
    {
        if (reloadShot <= 0)
        {
            //no rotate
            var number = Random.Range(0, 3);

            

            if (number == 0)
            {

                objectFromPool.SpawnPool("green", transform.position, Quaternion.identity);
                Debug.Log("1");
            }
            else if (number == 1)
            {
                objectFromPool.SpawnPool("skull", transform.position, Quaternion.identity);


            }
            else if (number == 2)
            {
                //shootProjectiles.NumberOfProjectile = 3;
                //shootProjectiles.numOfProjectile = 3;
                shootProjectiles.Range = 5;
                shootProjectiles.SpeedBullet = 3;
                shootProjectiles.numOfProjectile = 3;
                //Projectiles.instance.NumberOfProjectile = 3;
                //Projectiles.instance.Range = 5;
                //Projectiles.instance.SpeedBullet = 3;


                objectFromPool.SpawnPool("ThreePro", transform.position, Quaternion.identity);
                //FireThreeProjectile();
                Debug.Log("2.");

            }
            else if (number == 3)
            {

                FireEightProjectile();
                Debug.Log("3");

            }
            else if (number == 4)
            {
                
                Debug.Log("4");
            }

            reloadShot = startShot;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }

    }

}
