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
    private bool isAlive;
    private bool facingR;
    private float distance;
    private GameObject targetPlayer;
    EnemyFlip flip;

    public GameObject TargetPlayer
    {
        get
        {
            return targetPlayer;
        }

        set
        {
            targetPlayer = value;
        }
    }

    // Use this for initialization
    public void ObjectSpawn() {
        rb2d = GetComponent<Rigidbody2D>();
        isAlive = true;
        facingR = true;
        TargetPlayer = FindTheClosestPlayer();
        StartCoroutine(Flip());
        heal = 1;
	}

	void FixedUpdate () {
        
        isOnGround = IsGround();
        if (isOnGround)
        {
            rb2d.velocity += Vector2.up * jumpforce;
        }
        distance = targetPlayer.transform.position.x - transform.position.x;
        Movement();
        Dead();
    }

    private void Movement()
    {
        
        if (!facingR)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (facingR)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
    public GameObject FindTheClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        GameObject targetPlayer = null;
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Vector2.Distance(this.transform.position, players[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetPlayer = players[i];
            }
        }
        return targetPlayer;
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

    IEnumerator Flip()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(.3f);
            Debug.Log("FacingR" + facingR);
            Debug.Log("Distance" + distance);
            if (facingR && (distance > 0))
            {
                
                Vector3 Scale = transform.localScale;
                if (Scale.x > 0)
                {
                    Scale.x *= -1;
                }
                transform.localScale = Scale;
                facingR = false;
            }

            else if (!facingR && (distance < 0) || facingR && (distance < 0))
            {
                Vector3 Scale = transform.localScale;
                if (Scale.x > 0)
                {
                    Scale.x *= -1;
                }
                transform.localScale = Scale;
                facingR = true;
            }
        }

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
            isAlive = false;
        }
    }
}
