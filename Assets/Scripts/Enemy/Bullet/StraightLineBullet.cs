using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineBullet : MonoBehaviour, IFPoolObject
{

    public float speed;
    Rigidbody2D rb2d;
    private Transform player;
    private Vector3 dir;

	public void ObjectSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb2d = GetComponent<Rigidbody2D>();
        dir = Vector3.Normalize(player.position - this.transform.position);
        Invoke("Disappear", 15);
    }
	
	void FixedUpdate () {
        StraightBullet(dir);
    }

    void StraightBullet(Vector3 dir)
    {   
        rb2d.velocity = dir*speed;
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
