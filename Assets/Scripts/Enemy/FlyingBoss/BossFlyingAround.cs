using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyingAround : MonoBehaviour {

    private BossFlyingMovement bossFlyingMovement;
    private BossHealth bossHealth;
    private Rigidbody2D rg2d;

    private float speed = 5f;
    private float radius = 0.3f;
    private Vector2 center;
    private float angle;
    

	// Use this for initialization
	void Start () {
        bossFlyingMovement = GetComponent<BossFlyingMovement>();
        bossHealth = GetComponent<BossHealth>();
        rg2d = GetComponentInParent<Rigidbody2D>();
        center = transform.position;
	}

    void Update()
    {
        angle += Time.deltaTime * speed;
    }
    void FixedUpdate () {
        
        if (bossFlyingMovement.CurrentState == BossFlyingMovement.State.MovingAroundMap || bossHealth.Health % ( bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 1 && bossFlyingMovement.CurrentState == BossFlyingMovement.State.Moving)
        {
            //StartCoroutine("Movement");
            Movement();
        }
	}

    void Movement()
    {
        
        bossFlyingMovement.CurrentState = BossFlyingMovement.State.MovingAroundMap;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = center + offset;

        //yield return new WaitForSeconds(10f);
        //bossFlyingMovement.CurrentState = BossFlyingMovement.State.Moving;
    }
}
