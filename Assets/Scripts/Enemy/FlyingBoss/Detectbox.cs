using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectbox : MonoBehaviour {

    //Movement
    public float horSpeed;
    public float verSpeed;
    public float range;
    private Vector2 CurPosition;
    private int numRound;
    private bool rotateMove;
    private bool waitSec;
    private bool MoveR;

    public Transform[] destination;

    // Use this for initialization
    void Start () {
        CurPosition = transform.position;
        horSpeed = 8;
        verSpeed = 4;
        range = 1;
        numRound = 0;
        waitSec = true;
        rotateMove = true;
        MoveR = true;
    }
	
	// Update is called once per frame
	void Update () {

        Controll();
    }

    void Controll()
    {

        if (waitSec)
        {

            //Move();
            if(MoveR)
                CurPosition.x += horSpeed * Time.deltaTime;
            else if (!MoveR)
                CurPosition.x -= horSpeed * Time.deltaTime;


            CurPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verSpeed) * range;
            transform.position = CurPosition;
        }
        
    }


    /*void Move()
    {
        if (CurPosition.y <= 0)
        {
            if (!check)
            {

                numRound++;
                check = true;
            }
            if (numRound % 4 == 2)
            {
                CurPosition.x -= horSpeed * Time.deltaTime;
            }
            else if (numRound % 4 == 0)
            {
                CurPosition.x += horSpeed * Time.deltaTime;
            }
        }
        else if (CurPosition.y > 0)
        {
            if (check)
            {

                numRound++;
                check = false;
            }
            if (numRound % 4 == 1)
            {
                CurPosition.x += horSpeed * Time.deltaTime;
            }
            else if (numRound % 4 == 3)
            {
                CurPosition.x -= horSpeed * Time.deltaTime;
            }
        }
        CurPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verSpeed) * range;
        transform.position = CurPosition;
    }
    void MoveOp()
    {
        if (CurPosition.y <= 0)
        {
            if (!check)
            {

                numRound++;
                check = true;
            }
            if (numRound % 4 == 2)
            {
                CurPosition.x += horSpeed * Time.deltaTime;
            }
            else if (numRound % 4 == 0)
            {
                CurPosition.x -= horSpeed * Time.deltaTime;
            }
        }
        else if (CurPosition.y > 0)
        {
            if (check)
            {

                numRound++;
                check = false;
            }
            if (numRound % 4 == 1)
            {
                CurPosition.x -= horSpeed * Time.deltaTime;
            }
            else if (numRound % 4 == 3)
            {
                CurPosition.x += horSpeed * Time.deltaTime;
            }
        }
        CurPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verSpeed) * range;
        transform.position = CurPosition;
    }
    */

    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WallEast"))
        {
           
            MoveR = false;
            waitSec = false;
           // StartCoroutine(MoveTo(destination[0].position, 2f));
            
            
        }
        else if (collision.CompareTag("WallWest"))
        {
            
            MoveR = true;
            waitSec = false;
            //StartCoroutine(MoveTo(destination[1].position, 2f));
            
        }
        
    }

    IEnumerator StopforASec()
    {
        
        yield return new WaitForSeconds(3);
        waitSec = true;
        
    }
    IEnumerator MoveTo(Vector3 des, float speed)
    {
        Debug.Log("Move1");
        while (transform.position != des)
        {
            Debug.Log("Move2");
            transform.position = Vector3.MoveTowards(transform.position, des, speed);
            yield return null; 
        }
    }
    
}
