using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour,IFPoolObject {

    // Use this for initialization
    public void ObjectSpawn()
    {
        
	}

    

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
