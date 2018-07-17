using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeliton : Minions
{
    Rigidbody2D rg2d;
    EnemyFlip flip;
    [SerializeField]
    private float speed;
	// Use this for initialization
	void Start () {
        flip = GameObject.Find("SkelitonGuy").GetComponent<EnemyFlip>();
        rg2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        
        if (flip.facingR)
        {
            rg2d.velocity = Vector2.right * speed * Time.deltaTime;
        }
        else if(!flip.facingR)
        {
            rg2d.velocity = Vector2.left * speed * Time.deltaTime;   
        }
        
    }
    
}
