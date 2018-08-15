using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHead : MonoBehaviour {
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Boot check tag: " + other.gameObject.tag);

        if (other.gameObject.tag != "Tile")
        {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<BoxCollider2D>(), true);
        }
    }
}
