using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
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
    private string playerTag;
    [SerializeField]
    private float groundedSkin = 0.05f;
    [SerializeField]
    private float shotDelay = 0.1f;
    [SerializeField]
    private float xMin = -21.5f;
    [SerializeField]
    private float xMax = 21.5f;
    [SerializeField]
    private Vector3 offsetArrow; 

    private bool isOnGround;
    private bool faceRight;
    private bool jumpRequest;
    private bool isInvicible;
    private float timeStamp;
    private Animator anim;
    private BoxCollider2D playerBox;
    private Rigidbody2D rb;
    private Vector2 playerSize;
    private Vector2 boxSize;
    private Vector2 playerBoxOffset;
    private PlayerAttribute playerAttr;
    private PlayerAttack playerAttack;

    ProjectilePool arrowPool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerBox = GetComponent<BoxCollider2D>();
        playerSize = playerBox.size;
        playerBoxOffset = playerBox.offset;
        boxSize = new Vector2(playerSize.x, groundedSkin);

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
        isOnGround = false;
        jumpRequest = false;

        arrowPool = ProjectilePool.Instance;
    }

    void Update()
    {
        PlayerIndex singlePlayer = (PlayerIndex)0;
        GamePadState testState = GamePad.GetState(singlePlayer);

        if (testState.IsConnected)
        {
            Debug.Log("Joy connected: " + singlePlayer);
        }

        else
        {
            Debug.Log("No Joy connected");
        }

        if (useJoyStick)
        {
            SwitchToJoyController();
        }

        if (Input.GetKeyDown(button.jumpButton) && isOnGround)
        {
            jumpRequest = true;
        }

        if (Input.GetKeyDown(button.meleeAtkButton))
        {
            MeleeAttack();
        }

        if (Input.GetKeyDown(button.rangeAtkButton) && playerAttr.Arrow > 0 && Time.time >= timeStamp)
        {
            RangeAttack();
            playerAttr.Arrow = -1;
            Debug.Log("Arrow: " + playerAttr.Arrow);

            timeStamp = Time.time + shotDelay;
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis(button.horizontalAxis);
        anim.SetFloat("Speed", Math.Abs(horizontal));
        MoveHorizontal(horizontal);
        Flip(horizontal);

        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * playerAttr.JumpPower, ForceMode2D.Impulse);

            jumpRequest = false;
            isOnGround = false;
        }
        else
        {
            Vector2 boxCenter = ((Vector2)transform.position + playerBoxOffset) + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            isOnGround = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer) != null);
        }
    }

    void MoveHorizontal(float dirHorizontal)
    {
        Vector2 moveVel = rb.velocity;
        moveVel.x = dirHorizontal * playerAttr.Speed;
        rb.velocity = moveVel;

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
            GameObject arrow = arrowPool.GetElementInPool(tag, transform.position + offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, -90f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.right);
            arrow.GetComponent<Arrow>().Speed = playerAttr.ShootSpeed;
        }
        else if (!faceRight)
        {
            Debug.Log(arrowPool);
            GameObject arrow = arrowPool.GetElementInPool(tag, transform.position - offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, 90f))) as GameObject;
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
        button.rangeAtkButton = "J" + playerTag + "YButton";
    }

    public IEnumerator Hurt(float duration)
    {
        isInvicible = true;
        GetComponent<Animation>().Play("GetDamage");

        yield return new WaitForSeconds(duration);

        isInvicible = false;
        GetComponent<Animation>().Stop("GetDamage");

        yield return null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("NoneEffectOnPlayer"))
        {
            Debug.Log("Ignore collision");
            Physics2D.IgnoreCollision(playerBox, collision, true);
        }    
    }
}
