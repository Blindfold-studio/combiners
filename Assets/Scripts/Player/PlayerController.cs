using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
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
    private string tag;
    [SerializeField]
    private float groundedSkin = 0.05f;
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
    private Rigidbody2D rb;
    private Vector2 playerSize;
    private Vector2 boxSize;
    private PlayerAttribute playerAttr;
    private PlayerAttack playerAttack;

    ItemAndEnemyPooler arrowPool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<BoxCollider2D>().size;
        boxSize = new Vector2(playerSize.x, groundedSkin);

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

        arrowPool = ItemAndEnemyPooler.Instance;
    }

    void Update()
    {
        if (Input.GetButtonDown(button.jumpButton) && isOnGround)
        {
            jumpRequest = true;
        }

        if (Input.GetButtonDown(button.meleeAtkButton))
        {
            MeleeAttack();
        }

        if (Input.GetButtonDown(button.rangeAtkButton) && playerAttr.Arrow > 0)
        {
            RangeAttack();
            playerAttr.Arrow = -1;
            Debug.Log("Arrow: " + playerAttr.Arrow);
        }
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis(button.horizontalAxis);
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
            Vector2 boxCenter = (Vector2)transform.position + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
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
            GameObject arrow = arrowPool.GetArrowInPool(tag, transform.position + offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, -90f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.right);
            arrow.GetComponent<Arrow>().Speed = playerAttr.ShootSpeed;
        }
        else if (!faceRight)
        {
            Debug.Log(arrowPool);
            GameObject arrow = arrowPool.GetArrowInPool(tag, transform.position - offsetArrow, Quaternion.Euler(new Vector3(0f, 0f, 90f))) as GameObject;
            arrow.GetComponent<Arrow>().SetDirection(Vector2.left);
            arrow.GetComponent<Arrow>().Speed = playerAttr.ShootSpeed;
        }
    }

    public bool IsInvicble()
    {
        return isInvicible;
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
}
