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
    private float widthX = 18f;
    [SerializeField]
    private float centerY = 0f;
    [SerializeField]
    private float widthY = 5f;
    [SerializeField]
    private float speed = 3f;

    public GameObject[] bossCheckBox;
    private int pattern;
    [SerializeField]
    private int currentPosition;
    [SerializeField]
    private float speedCheckBox = 30;
    [SerializeField]
    private float speedUpdate;
    [SerializeField]
    private float stop;

	// Use this for initialization
	void Start () {
        bossFlyingMovement = GetComponent<BossFlyingMovement>();
        bossHealth = GetComponent<BossHealth>();
        rg2d = GetComponent<Rigidbody2D>();
        alpha = 0f;
        speed = 150f;
        pattern = 0;
    }

    void Update()
    {

        if (bossFlyingMovement.CurrentState ==  BossFlyingMovement.State.MoveCircle)
        {
            bossFlyingMovement.initiatePoint += Time.deltaTime;
            widthX -= Time.deltaTime * 2f;
        }
        else if (bossFlyingMovement.CurrentState == BossFlyingMovement.State.MoveToCheckBox || bossFlyingMovement.CurrentState == BossFlyingMovement.State.HorizontalMove)
        {
            speedUpdate = Time.deltaTime * speedCheckBox;
        }
    }

    void FixedUpdate () {
        
        if (bossFlyingMovement.CurrentState != BossFlyingMovement.State.HorizontalMove && (bossFlyingMovement.CurrentState == BossFlyingMovement.State.MoveCircle || bossHealth.Health % ( bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 1 ))
        {
            CircleMovement();
            
        }
        else if (bossHealth.Health % (bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 2)
        {
            bossFlyingMovement.CurrentState = BossFlyingMovement.State.MoveToCheckBox;
            MoveToCheckBox();
        }
        else if (bossFlyingMovement.CurrentState == BossFlyingMovement.State.HorizontalMove)
        {
            MoveToCheckBox();
            Debug.Log("HORIZONTALMOVEMENT");
        }

       
	}

    void MoveToCheckBox()
    {
        
        if (bossFlyingMovement.inPlayer1)
        {
            bossCheckBox[pattern].transform.position = new Vector3(0f, 50f, 0f);
            transform.position = Vector3.MoveTowards(transform.position, bossCheckBox[pattern].transform.GetChild(currentPosition).transform.position, speedUpdate);
        }
        else
        {
            bossCheckBox[pattern].transform.position = new Vector3(0f, 0f, 0f);
            transform.position = Vector3.MoveTowards(transform.position, bossCheckBox[pattern].transform.GetChild(currentPosition).transform.position, speedUpdate);
        }


        if (currentPosition == bossCheckBox[pattern].transform.childCount - 1)
        {
            currentPosition = bossCheckBox[pattern].transform.childCount - 1;
        }
        else if (Vector3.Distance(bossCheckBox[pattern].transform.GetChild(currentPosition).transform.position, transform.position) <= stop)
        {
            currentPosition++;
        }


    }

    void CircleMovement()
    {
        
        bossFlyingMovement.CurrentState = BossFlyingMovement.State.MoveCircle;
        transform.position = new Vector2(centerX + (widthX * Mathf.Sin(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)), bossFlyingMovement.curPosition.y + (widthY * Mathf.Cos(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)));

        if (widthX <= -20)
        {
            if (bossFlyingMovement.inPlayer1)
            {
                transform.position = new Vector3(-20f, 47f, 0f);
            }
            else
            {
                transform.position = new Vector3(-20f, -3f, 0f);
            }

            bossFlyingMovement.CurrentState = BossFlyingMovement.State.HorizontalMove;
            pattern = 1;
        }
        currentPosition = 0;

    }

    

}
