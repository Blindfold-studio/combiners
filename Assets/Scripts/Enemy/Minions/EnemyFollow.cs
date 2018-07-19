using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : Minions {

    private GameObject player;
    private Vector2 target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float stop;

	void Start () {
        heal = 1;
    }

	void Update () {
        FollowPlayer();
        Dead();
        
	}

    void FollowPlayer()
    {
        player = FindClosetPlayer();
        Movement();
    }

    void Movement()
    {
        if(Vector2.Distance(player.transform.position, transform.position) > stop)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = this.transform.position;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            TakeDamage();
        }
        
    }

    
    public void Dead()
    {
        if (heal == 0)
        {
            Destroy(gameObject);
            DropItem(this.transform);
        }
    }

}
