using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundBullet : MonoBehaviour, IFPoolObject
{

    private int numOfProjectile;
    private int rangeOfProjectile;
    private float speedbullet;
    Rigidbody2D rb2d;
    private static GameObject player;
    ProjectilePool pool;

    GameObject atkBoss;
    GameObject bullet;
    Detectbox detectBox;

    Vector3 projectileVec;
    Vector3 projectileDir;

    
    // Use this for initialization
    public void ObjectSpawn()
    {
        atkBoss = GameObject.Find("Bigblue");
        detectBox = atkBoss.GetComponent<Detectbox>();
        player = detectBox.TargetPlayer;
        pool = ProjectilePool.Instance;

        //rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        
        //rb2d.velocity = projectileDir * Time.deltaTime;
        //rb2d.velocity = new Vector2(projectileDir.x, projectileDir.y);
        //Debug.Log(rb2d.velocity);
    }

    //3Projectile----
    public void FireThreeProjectile()
    {
        
        numOfProjectile = 3;
        rangeOfProjectile = 5;
        speedbullet = 3;
        
        SetProjectile( numOfProjectile, rangeOfProjectile, speedbullet);
    }

    //8Projectile
    public void FireEightProjectile()
    {
        numOfProjectile = 8;
        rangeOfProjectile = 30;
        speedbullet = 3;
       
        SetProjectile( numOfProjectile, rangeOfProjectile, speedbullet);
    }

    //Manyprojectile
    public void SetProjectile( int numOfP, float range, float speedBullet)
    {
        
        float angleTotal = 360f / numOfP;
        float angle = 0f;
        Debug.Log(numOfP);
        for (int i = 0; i <= numOfP - 1; i++)
        {
            float projectileX = player.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * range;
            float projectileY = player.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * range;

            projectileVec = new Vector3(projectileX, projectileY, 0);
            projectileDir = Vector3.Normalize(projectileVec - this.transform.position) * speedBullet;


            //var proj = pool.GetElementInPool("Boss-Many", transform.position, Quaternion.identity);
            //proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);
            bullet = pool.GetElementInPool("Boss-Many", atkBoss.transform.position, Quaternion.identity);
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
