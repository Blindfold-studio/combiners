using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D rb2d;
    

    [SerializeField]
    private float speed;

    [SerializeField]
    private Transform[] groundGroup;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask isGroundnow;

    private bool isOnGround;

    private bool jump;
    [SerializeField]
    private float jumpforce;

    // Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();	
	}

    // Update is called once per frame

    void Update()
    {
        InputKey();
    }

    void FixedUpdate () {
        float DirLR = Input.GetAxis("Horizontal");

        isOnGround = IsGround();

       
        Movement(DirLR);
        ResetVariable();
        
    }

    private void ResetVariable()
    {
        jump = false;
    }
    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            Debug.Log("jump");
        }
    }

    private void Movement(float DirLR)
    {
        rb2d.velocity = new Vector2(DirLR * speed, rb2d.velocity.y);
    
        if (isOnGround && jump)
        {
            isOnGround = false;
            rb2d.AddForce(new Vector2(0, jumpforce));
        }
    }

    private bool IsGround()
    {
        if(rb2d.velocity.y <= 0)
        {
            foreach(Transform point in groundGroup)
            {
                Collider2D[] col2d = Physics2D.OverlapCircleAll(point.position, groundRadius, isGroundnow); 

                for(int i = 0; i < col2d.Length; i++)
                {
                    if(col2d[i].gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;

    }
}
