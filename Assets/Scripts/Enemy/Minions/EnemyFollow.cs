using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour {

    private GameObject player;
    private Vector2 target;
    [SerializeField]
    private float speed;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        target = new Vector2(player.transform.position.x, player.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
        FollowPlayer();	
	}

    void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
