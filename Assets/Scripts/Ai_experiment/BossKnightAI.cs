using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightAI : MonoBehaviour {
    [SerializeField]
    private float speed = 2f;
    private Rigidbody2D rb;

    public bool isFacingRight;
	public StateMachine<BossKnightAI> stateMachine { get; set; }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    void Start ()
    {
        isFacingRight = false;

        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine<BossKnightAI>(this);
        stateMachine.ChangeState(BossKnightMoveState.Instance);
    }

    void Update ()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
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

    public Rigidbody2D Rb
    {
        get
        {
            return rb;
        }
    }
}
