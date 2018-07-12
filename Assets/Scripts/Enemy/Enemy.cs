using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public float stopping;
    public float retreat;

    public float startShot;
    public float reloadShot;

    public GameObject bullet;
    public GameObject bulletFollow;
    private Transform player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        reloadShot = startShot;
	}
	
	// Update is called once per frame
	void Update () {
        EnemyMove();
        //EnemyShot();
	}

    void EnemyMove()
    {
        if(Vector2.Distance(transform.position, player.transform.position) > stopping)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < stopping && Vector2.Distance(transform.position, player.transform.position) > retreat)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < retreat)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
        }
    }

    /*void EnemyShot()
    {
        if (reloadShot <= 0)
        {
            //no rotate
            var number = Random.Range(0, 2);
            
            if (number == 1)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bulletFollow, transform.position, Quaternion.identity);
                
            }
            reloadShot = startShot;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }

    }*/
}
