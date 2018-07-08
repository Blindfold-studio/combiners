using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlip : MonoBehaviour {


    
    private Transform distanceToPlayer;

    private float distance;

    private bool facingR;

    void Start()
    {
        distanceToPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        facingR = false;    
    }

    // Update is called once per frame
    void Update () {
        distance = (distanceToPlayer.position.x - transform.position.x);
        
        FlipSide();
	}

    private void FlipSide()
    {
        
        if(!facingR && (distance <= 0))
        {
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
            facingR = true;
            
        }
        else if (facingR && (distance > 0))
        {
            Vector3 Scale = transform.localScale;
            Scale.x *= -1;
            transform.localScale = Scale;
            facingR = false;
            
        }
    }
}
