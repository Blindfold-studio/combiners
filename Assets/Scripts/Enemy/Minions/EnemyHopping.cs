using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHopping : Minions, IFPoolObject
{

    Rigidbody2D rb2d;

    [SerializeField]
    private Transform[] GroundDetect;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask isGroundnow;

    private bool jump;
    private bool isOnGround;

    [SerializeField]
    private float jumpforce;

    private GameObject player;
    public float speed;
    EnemyFlip flip;

	// Use this for initialization
	public void ObjectSpawn() {
        rb2d = GetComponent<Rigidbody2D>();
        flip = gameObject.GetComponent<EnemyFlip>();
        player = flip.FindClosetPlayer();
        heal = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
        isOnGround = IsGround();
        if (isOnGround)
        {
            rb2d.velocity += Vector2.up * jumpforce;
        }

        Movement();
        Dead();
    }

    private void Movement()
    {
        if (flip.facingR)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (!flip.facingR)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    private bool IsGround()
    {
        if (rb2d.velocity.y <= 0)
        { 
            foreach (Transform point in GroundDetect)
            {
                Collider2D[] col2d = Physics2D.OverlapCircleAll(point.position, groundRadius, isGroundnow);

                for (int i = 0; i < col2d.Length; i++)
                {
                    
                    if (col2d[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy") || collision.CompareTag("NoneEffectOnPlayer"))
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), collision, true);
            
        }
        else if (collision.CompareTag("Weapon"))
        {
            Debug.Log("GetintoSlimeWeapon");
            TakeDamage();
        }
    }
}
