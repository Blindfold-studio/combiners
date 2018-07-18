using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAtk : MonoBehaviour {
    [SerializeField]
    private float reloadShot;
    [SerializeField]
    private float startShot;

    GameObject objectPool;
    SurroundBullet surBullet;
    
    private GameObject player;
    ProjectilePool pool;

    private int numOfProjectile;
    private int rangeOfProjectile;
    private float speedbullet;
    Vector3 projectileVec;
    Vector3 projectileDir;
    GameObject bullet;

    void Start () {
        
        reloadShot = startShot;
        pool = ProjectilePool.Instance;
        player = GameObject.FindGameObjectWithTag("Player");
        //objectPool = pool.GetElementInPool("Boss-Many", this.transform.position, Quaternion.identity);

        //surBullet = objectPool.GetComponent<SurroundBullet>();
        //objectPool.SetActive(false);
        //surroundB = GameObject.Find("ObjectPooling");
        //sur = surroundB.GetComponent<SurroundBullet>();
	}

    void FixedUpdate()
    {
        EnemyShot();
        
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
                //Debug.Log(surroundB.GetComponent<Detectbox>().TargetPlayer);
                FireThreeProjectile();
               // FireThreeProjectile();
            }
            else if (number == 3)
            {
                FireEightProjectile();
                //FireEightProjectile();
            }
            

            reloadShot = startShot;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }

    }

    //3Projectile----
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


            //var proj = pool.GetElementInPool("Boss-Many", transform.position, Quaternion.identity);
            //proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);
            bullet = pool.GetElementInPool("Boss-Many", transform.position, Quaternion.identity);
            //rb2d.velocity = new Vector2(projectileDir.x, projectileDir.y);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);

            angle += angleTotal;

        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }   

}
