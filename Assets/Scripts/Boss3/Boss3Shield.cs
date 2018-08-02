using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Boss3Shield : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning(collision.gameObject.tag);

        if (collision.CompareTag("Weapon"))
        {
            collision.gameObject.SetActive(false);
        }
    }

}
