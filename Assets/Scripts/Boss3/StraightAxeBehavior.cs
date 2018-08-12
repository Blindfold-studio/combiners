using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightAxeBehavior : MonoBehaviour, IFPoolObject {

    private Rigidbody2D rb;
    [SerializeField]
    private float axeSpeed = 10f;
    private Animator animator;
    private Vector2 dir;

    public GameObject targetPlayer;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //this.gameObject.SetActive(false);
    }

    void Update ()
    {
        //Moving(targetPlayer);
        //Debug.Log("Straight Axe Moving");
    }

    void FixedUpdate ()
    {
        rb.velocity = dir * axeSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("hit player");
            animator.enabled = false;
            this.gameObject.SetActive(false);
        }
    }

	public void Moving(GameObject targetPlayer) {
        animator.enabled = true;
        if (targetPlayer.transform.position.x - transform.position.x < 0)
        {
            rb.velocity = new Vector2(-1 * axeSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(axeSpeed, rb.velocity.y);
        }
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public void ObjectSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //this.gameObject.SetActive(false);
        Debug.Log("Hello straight");
        animator.enabled = true;
    }

    public void SetTargetPlayer (GameObject targetPlayer)
    {
        this.targetPlayer = targetPlayer;
        if (targetPlayer.transform.position.x - transform.position.x < 0)
        {
            //rb.velocity = new Vector2(-1 * axeSpeed, rb.velocity.y);
            dir = new Vector2(-1f, rb.velocity.y);
        }
        else
        {
            //rb.velocity = new Vector2(axeSpeed, rb.velocity.y);
            dir = new Vector2(1f, rb.velocity.y);
        }
    }

    public void SetDirectionFromBoss (BossKnightAI boss)
    {
        if (!boss.isFacingRight)
        {
            //rb.velocity = new Vector2(-1 * axeSpeed, rb.velocity.y);
            dir = new Vector2(-1f, rb.velocity.y);
        }
        else
        {
            //rb.velocity = new Vector2(axeSpeed, rb.velocity.y);
            dir = new Vector2(1f, rb.velocity.y);
        }
    }
}
