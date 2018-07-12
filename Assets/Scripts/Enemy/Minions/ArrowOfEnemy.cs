using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOfEnemy : MonoBehaviour
{

    Rigidbody2D rb2d;

    [SerializeField]
    private float speed;

    EnemyFlip flip;
    private Vector2 dir;

    private Transform player;
    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        flip = GameObject.Find("cat").GetComponent<EnemyFlip>();
        if (flip.facingR)
        {
            dir = Vector2.right;
        }
        else
        {
            dir = Vector2.left;
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Dir();
    }

    void Dir()
    {
        if (flip.facingR)
        {
            rb2d.velocity = dir * speed;
        }
        else if (!flip.facingR)
        {
            rb2d.velocity = dir * speed;
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false); 
    }
}
