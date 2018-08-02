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

    public GameObject[] checkPoint;
    [SerializeField]
    private int currentPosition;
    [SerializeField]
    private float speedCheckPoint;
    [SerializeField]
    private float speedUpdate;
    [SerializeField]
    private float stop;

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
        if (bossFlyingMovement.CurrentState ==  BossFlyingMovement.State.MoveCircle)
        {
            bossFlyingMovement.initiatePoint += Time.deltaTime;
        }
        else if (bossFlyingMovement.CurrentState == BossFlyingMovement.State.Idle)
        {
            speedUpdate = Time.deltaTime * speedCheckPoint;
        }
    }
    void FixedUpdate () {
        
        if (bossHealth.Health % ( bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 1 )
        {
            Movement();
            
        }
        else if (bossHealth.Health % (bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 2)
        {
            MovingAround();
        }
       
	}

    void MovingAround()
    {
        bossFlyingMovement.CurrentState = BossFlyingMovement.State.Idle;
        transform.position = Vector3.MoveTowards(transform.position, checkPoint[currentPosition].transform.position, speedUpdate);
        
        if(Vector3.Distance(checkPoint[currentPosition].transform.position, transform.position) <= stop)
        {
            currentPosition++;
        }

        if( currentPosition >= checkPoint.Length)
        {
            currentPosition = 0;
        }
       
        
    }

    void Movement()
    {
        
        bossFlyingMovement.CurrentState = BossFlyingMovement.State.MoveCircle;
        
        transform.position = new Vector2(centerX + (widthX * Mathf.Sin(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)), bossFlyingMovement.curPosition.y + (widthY * Mathf.Cos(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)));

    }
}
