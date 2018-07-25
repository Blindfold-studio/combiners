using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLineBullet : MonoBehaviour,IFPoolObject {
    
    public float speed;
    private Transform player;
    private Vector2 target;
    private Vector3 dir;

  
    public void ObjectSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Invoke("Disappear", 7);
    }

    void Update()
    {
        FollowBullet();
    }

    void FollowBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    void Disappear()
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
