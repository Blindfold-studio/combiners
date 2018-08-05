using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloorScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Boot check tag: " + other.gameObject.tag);

        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss" || other.gameObject.tag == "Player" || other.gameObject.tag == "NoneEffectOnPlayer") {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<BoxCollider2D>(), true);
        }
    }
}
