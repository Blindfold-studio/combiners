using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Disappear", 15);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Disappear()
    {
        gameObject.SetActive(false);
    }
}
