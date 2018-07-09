using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOfEnemy : MonoBehaviour {

    Rigidbody2D rb2d;

    [SerializeField]
    private float speed;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        rb2d.velocity = Vector2.left * speed;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Weapon") || other.CompareTag("Player"))
        {
            Debug.Log("dead");
            gameObject.SetActive(false);
        }
    }
}
