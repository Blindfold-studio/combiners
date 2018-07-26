using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour,IFPoolObject {

    // Use this for initialization
    public void ObjectSpawn()
    {
        Invoke("Disappear", 7);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
