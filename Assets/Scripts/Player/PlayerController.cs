using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField]
    private string playerTag;
    [SerializeField]
    private bool useJoyStick;
    [System.Serializable]
    public class ButtonController
    {
        public string horizontalAxis;
        public string jumpButton;
        public string meleeAtkButton;
        public string rangeAtkButton;
    }

    public ButtonController button;
    public LayerMask groundLayer;
    public GameObject arrowPrefab;

    [SerializeField]
    private string arrowTag;
    [SerializeField]
    private float shotDelay = 0.1f;
    [SerializeField]
    private float knockbackForce;
    [SerializeField]
    private Vector3 offsetArrow; 

    public bool IsOnGround { get; set; }
    private bool faceRight;
    private bool jumpRequest;
    private bool isInvicible;
    private bool isKnockback;
    private float distanceToCamera;
    private float timeStamp;
    private float screenPadding = 0.5f;
    private float xMin;
    private float xMax;
    private Animator anim;
    private Rigidbody2D rb;
    private PlayerAttribute playerAttr;
    private PlayerAttack playerAttack;

    ProjectilePool arrowPool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        playerAttr = GetComponent<PlayerAttribute>();

        foreach (Transform child in gameObject.transform)
        {
            if (child.gameObject.CompareTag("Weapon"))
            {
                playerAttack = child.gameObject.GetComponent<PlayerAttack>();
            }
        }
    }

    void Start()
    {
        faceRight = true;
        isInvicible = false;
        IsOnGround = false;
        jumpRequest = false;

        arrowPool = ProjectilePool.Instance;

        SetPositionNotOverViewPort();
    }

    void Update()
    {
        if (useJoyStick)
        {
            SwitchToJoyController();
        }
        
        if (Input.GetButtonDown(button.jumpButton) && IsOnGround)
        {
            jumpRequest = true;
        }

        if (Input.GetButtonDown(button.meleeAtkButton))
        {
            MeleeAttack();
        }

        if (Input.GetButtonDown(button.rangeAtkButton) && playerAttr.Arrow > 0 && Time.time >= timeStamp)
        {
            RangeAttack();
            playerAttr.Arrow = -1;
            Debug.Log("Arrow: " + playerAttr.Arrow);

            timeStamp = Time.time + shotDelay;
        }
    }

    void FixedUpdate()
    {
        float horizontal = 0f;
        if (Math.Abs(Input.GetAxis(button.horizontalAxis)) <= Math.Abs(Input.GetAxis("J" + playerTag + "Horizontal")))
        {
            horizontal = Input.GetAxis("J" + playerTag + "Horizontal");
        }
        else
        {
            horizontal = Input.GetAxis(button.horizontalAxis);
        }

        anim.SetFloat("Speed", Math.Abs(horizontal));
        MoveHorizontal(horizontal);
        Flip(horizontal);

        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * playerAttr.JumpPower, ForceMode2D.Impulse);

            jumpRequest = false;
            IsOnGround = false;
        }
    }

    void MoveHorizontal(float dirHorizontal)
    {
        if (!isKnockback)
        {
            Vector2 moveVel = rb.velocity;
            moveVel.x = dirHorizontal * playerAttr.Speed;
            rb.velocity = moveVel;
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, xMin, xMax), transform.position.y, transform.position.z);
    }

    void Flip(float horizontal)
    {
        if ((horizontal > 0 && !faceRight) || (horizontal < 0 && faceRight))
        {
            faceRight = !faceRight;

            Vector2 playerScale = transform.localScale;
            playerScale.x *= -1;
            transform.localScale = playerScale;
        }
    }

    void MeleeAttack ()
    {
        playerAttack.SwordAttack();
    }

    void RangeAttack()
    {
        if (faceRight)
        {
            Debug.Log(arrowPool);
            GameObject arrow = arrowPool.GetElementInPool(arrowTag, transform.position + offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, -90f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.right);
            arrow.GetComponent<Arrow>().Speed = playerAttr.ShootSpeed;
        }
        else if (!faceRight)
        {
            Debug.Log(arrowPool);
            GameObject arrow = arrowPool.GetElementInPool(arrowTag, transform.position - offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, 90f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.left);
            arrow.GetComponent<Arrow>().Speed = playerAttr.ShootSpeed;
        }
    }

    public bool IsInvicble()
    {
        return isInvicible;
    }

    void SwitchToJoyController ()
    {
        button.horizontalAxis = "J" + playerTag + "Horizontal";
        button.jumpButton = "J" + playerTag + "AButton";
        button.meleeAtkButton = "J" + playerTag + "XButton";
        button.rangeAtkButton = "J" + playerTag + "BButton";
    }

    void SetPositionNotOverViewPort ()
    {
        distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMin = leftmost.x + screenPadding;
        xMax = rightmost.x - screenPadding;
        Debug.Log("x min: " + xMin + "x max: " + xMax);
    }

    void KnockBack ()
    {
        if (!faceRight)
        {
            rb.velocity = new Vector2(knockbackForce, knockbackForce);
        }

        else
        {
            rb.velocity = new Vector2(-knockbackForce, knockbackForce);
        }
    }

    public IEnumerator Hurt(float knockbackTime, float duration)
    {
        isInvicible = true;
        isKnockback = true;
        GetComponent<Animation>().Play("GetDamage");
        KnockBack();

        yield return new WaitForSeconds(knockbackTime);

        isKnockback = false;

        yield return new WaitForSeconds(duration - knockbackTime);

        isInvicible = false;
        GetComponent<Animation>().Stop("GetDamage");

        yield return null;
    }
}
