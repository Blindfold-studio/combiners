using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAxeBehavior : MonoBehaviour {

    [SerializeField]
    private float throwingAngle = 45f * Mathf.Deg2Rad;
    [SerializeField]
    private float gravity = 0.8f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
        this.gameObject.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            this.gameObject.SetActive(false);
        }
    }
	
	public void Moving(GameObject targetPlayer) {
        float verticalDistance =  targetPlayer.transform.position.y - this.transform.position.y;
        float horizontalDistance = targetPlayer.transform.position.x - this.transform.position.x;
        float vel = Mathf.Sqrt((gravity*horizontalDistance*horizontalDistance) / (1 + Mathf.Cos(2*throwingAngle)*(horizontalDistance*Mathf.Tan(throwingAngle) - verticalDistance)));
        Debug.Log(verticalDistance);
        Debug.Log(horizontalDistance);
        float vel_x = vel * Mathf.Cos(throwingAngle);
        float vel_y = vel * Mathf.Sin(throwingAngle);
        if(horizontalDistance < 0) {
            vel_x = vel * Mathf.Cos(180f*Mathf.Deg2Rad - throwingAngle);
            vel_y = vel * Mathf.Sin(180f*Mathf.Deg2Rad - throwingAngle);
        } 
        rb.velocity = new Vector2(vel_x, vel_y);
    }
}
