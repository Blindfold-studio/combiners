using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLineBullet : MonoBehaviour,IFPoolObject {
    
    public float speed;
    private Vector3 dir;
    private GameObject targetPlayer;

    public GameObject TargetPlayer
    {
        get
        {
            return targetPlayer;
        }

        set
        {
            targetPlayer = value;
        }
    }


    public void ObjectSpawn()
    {
        TargetPlayer = FindTheClosestPlayer();
        Invoke("Disappear", 7);
    }

    void Update()
    {
        FollowBullet();
    }

    void FollowBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, speed * Time.deltaTime);
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
