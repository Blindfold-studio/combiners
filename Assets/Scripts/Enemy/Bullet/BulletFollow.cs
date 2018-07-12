using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFollow : MonoBehaviour,IFPoolObject {

    public float speed;

    private Transform player;
    private Vector2 target;
    private Vector3 dir;

    ObjectPoolBullets objectPool;

    // Use this for initialization
    public void ObjectSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowBullet();
    }


    void FollowBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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


    
}
