using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour, IFPoolObject {

    public int NumberOfProjectile
    {
        get
        {
            return numOfProjectile;
        }

        set
        {
            numOfProjectile = value;
        }
    }
    public float Range
    {
        get
        {
            return range;
        }
        set
        {
            range = value;
        }
    }
    public float SpeedBullet
    {
        get
        {
            return speedBullet;
        }
        set
        {
            speedBullet = value;
        }
    }
    [SerializeField]
    public int numOfProjectile =3;
    [SerializeField]
    public float range =3 ;
    [SerializeField]
    public float speedBullet=3;
    Rigidbody2D rg2d;

    public static Projectiles instance;

    private GameObject player;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    public void ObjectSpawn ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rg2d = GetComponent<Rigidbody2D>();
        SetProjectile(numOfProjectile, range, speedBullet);
        Debug.Log("***************");
        Debug.Log("NumOF"+ NumberOfProjectile);
        //Debug.Log("Range" + range);
        //Debug.Log("Speed" + speedBullet);
        
	}
	
	// Update is called once per frame
	void Update () {
        
    }
    //Manyprojectile
    void SetProjectile(int numOfP, float range, float speedBullet)
    {
        float angleTotal = 360f / numOfP;
        float angle = 0f;
        Debug.Log("-----------");
        for (int i = 0; i <= numOfP - 1; i++)
        {
            float projectileX = player.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * range;
            float projectileY = player.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * range;

            Vector3 projectileVec = new Vector3(projectileX, projectileY, 0);
            Vector3 projectileDir = Vector3.Normalize(projectileVec - this.transform.position) * speedBullet;

            Debug.Log("-----------");
            Debug.Log("numOfP" + numOfP);
            Debug.Log("range" + range);
            Debug.Log("speedBullet" + speedBullet);
            
            //var proj = Instantiate(bullet3Pro, this.transform.position, Quaternion.identity);
            //proj.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileDir.x, projectileDir.y);
            rg2d.velocity = new Vector2(projectileDir.x, projectileDir.y);

            angle += angleTotal;

        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }



}
