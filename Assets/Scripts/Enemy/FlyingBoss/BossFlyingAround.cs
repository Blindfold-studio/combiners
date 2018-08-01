using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyingAround : MonoBehaviour {

    private BossFlyingMovement bossFlyingMovement;
    private BossHealth bossHealth;
    private Rigidbody2D rg2d;

    [SerializeField]
    private float alpha;
    [SerializeField]
    private float centerX = 0f;
    [SerializeField]
    private float widthX = 10f;
    [SerializeField]
    private float centerY = 0f;
    [SerializeField]
    private float widthY = 5f;
    [SerializeField]
    private float speed = 150f;
    private Vector3 setPosition;

	// Use this for initialization
	void Start () {
        bossFlyingMovement = GetComponent<BossFlyingMovement>();
        bossHealth = GetComponent<BossHealth>();
        rg2d = GetComponentInParent<Rigidbody2D>();
        alpha = 0f;
        speed = 150f;

    }

    void Update()
    {
        if (bossFlyingMovement.CurrentState ==  BossFlyingMovement.State.MovingAroundMap)
        {
            bossFlyingMovement.initiatePoint += Time.deltaTime;
        }
    }
    void FixedUpdate () {
        
        if (bossFlyingMovement.CurrentState == BossFlyingMovement.State.MovingAroundMap || bossHealth.Health % ( bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 1 && bossFlyingMovement.CurrentState == BossFlyingMovement.State.Moving)
        { 
            Movement();   
        }
        else if (bossFlyingMovement.CurrentState == BossFlyingMovement.State.MovingAroundMap)
        {
            
        }
	}

    void Movement()
    {
        
        bossFlyingMovement.CurrentState = BossFlyingMovement.State.MovingAroundMap;
        transform.position = new Vector2(centerX + (widthX * Mathf.Sin(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)), bossFlyingMovement.curPosition.y + (widthY * Mathf.Cos(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)));
 
    }
}
