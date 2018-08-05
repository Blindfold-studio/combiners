using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImproveJump : MonoBehaviour {
    [SerializeField]
    private bool useJoy;
    [SerializeField]
    private float fallMul = 2.5f;
    [SerializeField]
    private float lowJumpMul = 2f;
    [SerializeField]
    private string jumpButton;
    [SerializeField]
    private string jumpButtonJoy;

    private Rigidbody2D rb2d;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = fallMul;
        }
        else if (rb2d.velocity.y > 0 && useJoy ? !Input.GetButton(jumpButtonJoy) : !Input.GetButton(jumpButton))
        {
            rb2d.gravityScale = lowJumpMul;
        }
        else
        {
            rb2d.gravityScale = 1f;
        }
    }
}
