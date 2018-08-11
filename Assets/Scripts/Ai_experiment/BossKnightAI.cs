using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightAI : MonoBehaviour {
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float chargeSpeed;
    [SerializeField]
    private float chargeTimeLimit;
    [SerializeField]
    private float prepareAttackTime;
    [SerializeField]
    private float prepareToChargeTime;
    [SerializeField]
    private float shortAttackDuration;
    [SerializeField]
    private float stuntAfterPlayerJumpOverHead = 0.6f;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private GameObject shortWeapon;
    [SerializeField]
    private BoxCollider2D shortAttackBox;

    private bool onHoldForPlayerJump;
    private float distanceToCamera;
    private float screenPadding = 0.5f;
    private float xMin;
    private float xMax;
    private Rigidbody2D rb;
    private BossHealth bossHealth;

    public bool isFacingRight;
    public float idleStateTime;
    public float moveStateTime;
    public float ChargeSpeed { get { return chargeSpeed; } }
    public float ChargeTimeLimit { get { return chargeSpeed; } }
    public float PrepareAttackTime { get { return prepareAttackTime; } }
    public float PrepareToChargeTime { get { return prepareToChargeTime; } }
    public float ShortAttackDuration { get { return shortAttackDuration; } }
    public float XMin { get { return xMin; } }
    public float XMax { get { return xMax; } }
    public GameObject TargetPlayer { get; set; }
    public StateMachine<BossKnightAI> stateMachine { get; set; }

    void Start ()
    {
        isFacingRight = false;

        bossHealth = GetComponent<BossHealth>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine<BossKnightAI>(this);
        stateMachine.ChangeState(new BossKnightMoveState(this));
        shortWeapon.SetActive(false);
        SetPositionNotOverViewPort();
    }

    void Update ()
    {
        stateMachine.Update();

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, transform.position.z);
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

    public void SetActiveShield (bool value)
    {
        shield.SetActive(value);
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

    public BoxCollider2D GetShortAttackBox ()
    {
        return shortAttackBox;
    }

    public void MeleeAttack ()
    {
        float angleSword = isFacingRight ? -90f : 90f;
        shortWeapon.SetActive(true);
        shortWeapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleSword));
    }

    public void StopMeleeAttack ()
    {
        shortWeapon.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        shortWeapon.SetActive(false);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        stateMachine.OnTriggerEnter();
    }
}
