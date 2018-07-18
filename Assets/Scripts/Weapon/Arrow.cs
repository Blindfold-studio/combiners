using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Arrow : MonoBehaviour {
    [SerializeField]
    private int damage;

    private float speed;
    private Rigidbody2D rb;
    private Vector2 dir;
    

    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }

    public int Damage
    {
        get
        {
            return damage;
        }
        
        set
        {
            damage = value;
        }
    }

    public void SetDirection(Vector2 dir)
    {
        this.dir = dir;
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
        {
            gameObject.SetActive(false);
        } 
        else if (collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossHealth>().Health = -damage;
            gameObject.SetActive(false);
        }
    }
}
