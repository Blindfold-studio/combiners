using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAxeBehavior : MonoBehaviour, IFPoolObject {

    [SerializeField]
    private float throwingAngle = 60f * Mathf.Deg2Rad;
    [SerializeField]
    private float gravity = 0.8f;
    private float horizontalDistance;
    private float verticalDistance;
    private float minXDistance = 8f;
    private float maxXDistance = 14f;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject targetPlayer;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
        //this.gameObject.SetActive(false);
	}

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Axe: " + other.name);

        if (other.CompareTag("Player") || other.CompareTag("DestroyObject")) {
            animator.enabled = false;
            this.gameObject.SetActive(false);
        }
    }
	
    void Update ()
    {
        //Moving(targetPlayer);
        //Debug.Log("Projectile Axe Moving");
    }

    void FixedUpdate ()
    {
        //AxeMoving();
    }

	public void Moving(GameObject targetPlayer) {
        float verticalDistance =  targetPlayer.transform.position.y - this.transform.position.y;
        float horizontalDistance = targetPlayer.transform.position.x - this.transform.position.x;
        Debug.Log("Distance between boss and player: " + horizontalDistance);
        //float vel = Mathf.Sqrt((gravity*horizontalDistance*horizontalDistance) / (1 + Mathf.Cos(2*throwingAngle)*(horizontalDistance*Mathf.Tan(throwingAngle) - verticalDistance)));
        float vel = Mathf.Sqrt((Physics2D.gravity.y * horizontalDistance * horizontalDistance) / (1 + Mathf.Cos(2 * throwingAngle) * (horizontalDistance * Mathf.Tan(throwingAngle) - verticalDistance)));
        Debug.Log(verticalDistance);
        Debug.Log(horizontalDistance);
        float vel_x = vel * Mathf.Cos(throwingAngle);
        float vel_y = vel * Mathf.Sin(throwingAngle);
        //animator.enabled = true;
        if(horizontalDistance < 0) {
            vel_x = vel * Mathf.Cos(180f*Mathf.Deg2Rad - throwingAngle);
            vel_y = vel * Mathf.Sin(180f*Mathf.Deg2Rad - throwingAngle);
        } 
        rb.velocity = new Vector2(vel_x, vel_y);
    }

    public void AxeMoving(GameObject targetPlayer)
    {
        SetTargetPlayer(targetPlayer);
        //float verticalDistance = targetPlayer.transform.position.y - this.transform.position.y;
        //float horizontalDistance = targetPlayer.transform.position.x - this.transform.position.x;
        float vel = Mathf.Sqrt((gravity * horizontalDistance * horizontalDistance) / (1 + Mathf.Cos(2 * throwingAngle) * (horizontalDistance * Mathf.Tan(throwingAngle) - verticalDistance)));
        Debug.Log(verticalDistance);
        Debug.Log(horizontalDistance);
        float vel_x = vel * Mathf.Cos(throwingAngle);
        float vel_y = vel * Mathf.Sin(throwingAngle);
        animator.enabled = true;
        
        if (horizontalDistance < 0)
        {
            vel_x = vel * Mathf.Cos(180f * Mathf.Deg2Rad - throwingAngle);
            vel_y = vel * Mathf.Sin(180f * Mathf.Deg2Rad - throwingAngle);
        }
        rb.velocity = new Vector2(vel_x, vel_y);
        
        //rb.AddForce(new Vector2(vel_x, vel_y), ForceMode2D.Impulse);
    }

    public void ObjectSpawn()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //this.gameObject.SetActive(false);
        Debug.Log("Hello projectile");
    }

    void SetTargetPlayer(GameObject targetPlayer)
    {
        this.targetPlayer = targetPlayer;
        horizontalDistance = targetPlayer.transform.position.x - this.transform.position.x;
        verticalDistance = targetPlayer.transform.position.y - this.transform.position.y;
        Debug.Log("Distance: " + horizontalDistance);
        AdjustHorizontalDistance();
    }

    void AdjustHorizontalDistance ()
    {
        if (Mathf.Abs(horizontalDistance) >= maxXDistance)
        {
            horizontalDistance = (horizontalDistance >= 0) ? maxXDistance : -maxXDistance;
        }

        else if (Mathf.Abs(horizontalDistance) <= minXDistance)
        {
            horizontalDistance = (horizontalDistance >= 0) ? minXDistance : -minXDistance;
        }
    }
}
