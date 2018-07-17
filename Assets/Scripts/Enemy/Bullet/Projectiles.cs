using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour, IFPoolObject {

    
    Rigidbody2D rg2d;
    public static Projectiles instance;

    private GameObject player;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    public void ObjectSpawn ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rg2d = GetComponent<Rigidbody2D>();
        
        
        
	}
	



}
