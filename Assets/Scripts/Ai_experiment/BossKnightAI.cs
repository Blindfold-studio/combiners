using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightAI : MonoBehaviour {
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float stuntAfterPlayerJumpOverHead = 0.6f;

    private bool onHoldForPlayerJump;
    private float distanceToCamera;
    private float screenPadding = 0.5f;
    private float xMin;
    private float xMax;
    private Rigidbody2D rb;
    private BossHealth bossHealth;

    public bool isFacingRight;
    public GameObject TargetPlayer { get; set; }
    public StateMachine<BossKnightAI> stateMachine { get; set; }

    void Start ()
    {
        isFacingRight = false;

        bossHealth = GetComponent<BossHealth>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine<BossKnightAI>(this);
        stateMachine.ChangeState(BossKnightMoveState.Instance);
        SetPositionNotOverViewPort();
    }

    void Update ()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    void SetPositionNotOverViewPort()
    {
        distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftmost.x + screenPadding;
        xMax = rightmost.x - screenPadding;
        Debug.Log("x min: " + xMin + "x max: " + xMax);
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

    public IEnumerator FlipCharacter(float horizontalMovement)
    {
        if ((isFacingRight && horizontalMovement < 0) ||
           (!isFacingRight && horizontalMovement > 0))
        {
            onHoldForPlayerJump = true;
            isFacingRight = !isFacingRight;
            yield return new WaitForSeconds(stuntAfterPlayerJumpOverHead);
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            onHoldForPlayerJump = false;
        }
    }

    public void MoveTowardPlayer()
    {
        float vel = speed;
        if (!isFacingRight && speed > 0)
        {
            vel *= -1;
        }
        else if (isFacingRight && speed < 0)
        {
            vel *= -1;
        }
        rb.velocity = new Vector2(vel, rb.velocity.y);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, transform.position.z);
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public Rigidbody2D Rb
    {
        get
        {
            return rb;
        }
    }
}
