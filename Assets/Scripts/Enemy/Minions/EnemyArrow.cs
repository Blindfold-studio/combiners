using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    //Rigidbody2D rb2d;

    [SerializeField]
    private float speed;

    public float startShotReload;
    private float reloadShot;

    public GameObject arrow;

	// Use this for initialization
	void Start () {
        //rb2d = GetComponent<Rigidbody2D>();
        reloadShot = startShotReload;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       // rb2d.velocity = Vector2.left * speed;
        EnemyShot();
	}

    void EnemyShot()
    {
        if(reloadShot <= 0)
        {
            Debug.Log("Createarrow");
            Instantiate(arrow, transform.position,Quaternion.identity);
            reloadShot = startShotReload;
        }
        else
        {
            reloadShot -= Time.deltaTime;
        }
    }
}
