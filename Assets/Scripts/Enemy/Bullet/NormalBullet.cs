using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour,IFPoolObject {

    // Use this for initialization
    public void ObjectSpawn()
    {
        Invoke("Disappear", 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Disappear()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
}
