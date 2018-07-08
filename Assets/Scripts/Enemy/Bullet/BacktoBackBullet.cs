using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacktoBackBullet : MonoBehaviour, IFPoolObject
{
    
    
    private float speed = 2;
    private Transform player;
    private Vector3 target;
    private bool secondMove;
    private bool lastMove;

    Rigidbody2D rb2d;
    private Vector3 Dir;

    // Use this for initialization
    public void ObjectSpawn()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        rb2d = GetComponent<Rigidbody2D>();
        secondMove = true;
        lastMove = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if(secondMove && (transform.position != target))
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            
        }
        
        
        else if((transform.position == target) && secondMove)
        {
            
            Dir = Vector3.Normalize(player.position - this.transform.position) * speed * Time.deltaTime;
            
            StepTwo();
        }

        else if (lastMove)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            rb2d.velocity = new Vector2(Dir.x, Dir.y);
        }

    }

    void StepTwo()
    {
        
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        
        //dir = Vector3.Normalize(player.position - this.transform.position);

        secondMove = false;
        lastMove = true;
    }
}
