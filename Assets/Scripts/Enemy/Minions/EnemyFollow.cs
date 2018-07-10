using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    private GameObject player;
    private Vector2 target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float stop;
	// Use this for initialization
	void Start () {
                
    }
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();	
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon"))
        {
            Debug.Log("dead");
            gameObject.SetActive(false);
        }
    }
}
