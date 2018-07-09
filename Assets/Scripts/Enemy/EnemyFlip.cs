using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlip : MonoBehaviour {


    private GameObject player;
    private Transform distanceToPlayer;

    private float distance;

    private bool facingR;

    void Start()
    {
        
        facingR = false;    
    }

    // Update is called once per frame
    void Update () {
        player = FindClosetPlayer();
        distance = player.transform.position.x - transform.position.x;
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

    public GameObject FindClosetPlayer()
    {
        GameObject[] playerTarget;
        playerTarget = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject target in playerTarget)
        {
            Vector3 diff = target.transform.position - this.transform.position;
            float curDis = diff.sqrMagnitude;
            if (curDis < distance)
            {
                closest = target;
                distance = curDis;
            }
        }
        return closest;
    }
}
