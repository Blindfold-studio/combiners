using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : Minions
{

    //Rigidbody2D rb2d;

    [SerializeField]
    private float speed;

    public float startShotReload;
    private float reloadShot;

    public GameObject arrow;
    ProjectilePool pool;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
        reloadShot = startShotReload;
        pool = ProjectilePool.Instance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       // rb2d.velocity = Vector2.left * speed;
        EnemyShot();
	}

    void EnemyShot()
    {
        if(reloadShot <= 0)
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
