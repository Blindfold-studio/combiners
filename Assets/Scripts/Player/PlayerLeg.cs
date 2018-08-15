using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeg : MonoBehaviour {

    private PlayerController player;
    private BoxCollider2D playerBox;
    private Vector2 playerSize;
    private Vector2 boxSize;
    private Vector2 playerBoxOffset;
    [SerializeField]
    private float groundedSkin = 0.05f;

    public LayerMask groundLayer;

    void Start () {
        player = GetComponentInParent<PlayerController>();
        playerBox = GetComponent<BoxCollider2D>();
        playerSize = playerBox.size;
        playerBoxOffset = playerBox.offset;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        player.IsOnGround = false;
    }

    private void FixedUpdate()
    {
        Vector2 boxCenter = ((Vector2)player.gameObject.transform.position + playerBoxOffset) + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
        player.IsOnGround = (Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer) != null);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Boot check tag: " + other.gameObject.tag);

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Player" || other.gameObject.tag == "NoneEffectOnPlayer")
        {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<BoxCollider2D>(), true);
        }
    }
}
