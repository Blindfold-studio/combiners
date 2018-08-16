using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlyingAround : MonoBehaviour {

    private BossFlyingMovement bossFlyingMovement;
    private BossHealth bossHealth;
    private Rigidbody2D rg2d;
    private Vector3 dir;


    [SerializeField]
    private float centerX = 0f;
    [SerializeField]
    private float widthX = 18f;
    [SerializeField]
    private float centerY = 0f;
    [SerializeField]
    private float widthY = 5f;
    [SerializeField]
    private float speed = 150f;
    [SerializeField]
    private float maxSpeed = 300f;
    private float timer = 2f;
    private bool reCurrent = true;

    public GameObject[] bossCheckBox;
    public int pattern;
    public int currentPosition;
    [SerializeField]
    private float speedCheckBox = 30;
    [SerializeField]
    private float speedUpdate;
    [SerializeField]
    private float stop;
    private BoxCollider2D bossCollider;

    // Use this for initialization
    void Start () {
        bossFlyingMovement = GetComponent<BossFlyingMovement>();
        bossHealth = GetComponent<BossHealth>();
        bossCollider = GetComponent<BoxCollider2D>();
        rg2d = GetComponent<Rigidbody2D>();
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
        
        if (bossFlyingMovement.CurrentState != BossFlyingMovement.State.HorizontalMove && (bossFlyingMovement.CurrentState == BossFlyingMovement.State.MoveCircle || bossHealth.Health % ( bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 3 ))
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                //CircleMovement();
                HorizontalMove();
            }
            else
            {
                transform.position += Vector3.up * 0.1f;
            }
            
            
        }
        else if (bossHealth.Health % (bossHealth.maxHealth / bossHealth.numberOfTimeBossSwap) == 5)
        {
            bossFlyingMovement.CurrentState = BossFlyingMovement.State.MoveToCheckBox;
            pattern = 0;
            MoveToCheckBox();
        }
        else if (bossFlyingMovement.CurrentState == BossFlyingMovement.State.HorizontalMove)
        {
            pattern = 2;
            MoveToCheckBox();
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
                bossCollider.enabled = true;
        }
            else if (Vector3.Distance(bossCheckBox[pattern].transform.GetChild(currentPosition).transform.position, transform.position) <= stop)
            {
                currentPosition++;
                bossCollider.enabled = false;
            }
       
        


    }

    void HorizontalMove()
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
        pattern = 2;
        currentPosition = 0;
    }
    void CircleMovement()
    {
        
        bossFlyingMovement.CurrentState = BossFlyingMovement.State.MoveCircle;
        
       
        if (widthX <= 5 && bossFlyingMovement.CurrentState == BossFlyingMovement.State.MoveCircle)
        {

            

        }
        else
        {
            if (speed < maxSpeed)
            {
                speed += 20 * Time.deltaTime;
            }
        }
        

        transform.position = new Vector2(centerX + (widthX * Mathf.Sin(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)), bossFlyingMovement.curPosition.y + (widthY * Mathf.Cos(Mathf.Deg2Rad * bossFlyingMovement.initiatePoint * speed)));

        currentPosition = 0;

    }

    public void RecurrentPosition()
    {
        currentPosition = 0;
        timer = 2;
        speed = 150;
        widthX = 20;
    }
   

    
    
    

}
