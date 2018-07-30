using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineBullet : MonoBehaviour, IFPoolObject
{

    public float speed;
    Rigidbody2D rb2d;
    private Transform player;
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
       
        rb2d = GetComponent<Rigidbody2D>();
        TargetPlayer = FindTheClosestPlayer();
        dir = Vector3.Normalize(targetPlayer.transform.position - this.transform.position);
        Invoke("Disappear", 15);
    }
	
	void FixedUpdate () {
        StraightBullet(dir);
    }

    void StraightBullet(Vector3 dir)
    {   
        rb2d.velocity = dir*speed;
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
