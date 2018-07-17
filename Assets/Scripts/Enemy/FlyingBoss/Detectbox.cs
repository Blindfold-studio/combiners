using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detectbox : Boss {

    [SerializeField]
    private float amplitudeX = 10.0f;
    [SerializeField]
    private float amplitudeY = 1.0f;
    [SerializeField]
    private float omegaX = 1.25f;
    [SerializeField]
    private float omegaY = 2.5f;
    private float index;
    private float x,y;
    private int count = 0;
    GameObject minion;
    SpawnEnemyFly minionFly;

    void Awake()
    {
        minion = GameObject.Find("SpawnEnemy-Fly");
        minionFly = minion.GetComponent<SpawnEnemyFly>();
        heal = 4;
    }

    void Update () {

        
        Controll();
        
       
    }

    void Controll()
    {
        Debug.Log("Heal " + heal);
        if (count%2==0)
        {
            index -= Time.deltaTime;
        }
        else if(count%2==1)
        {
            index += Time.deltaTime;
        }

        
        x = amplitudeX * Mathf.Cos(omegaX * index);
        if (CheckHealh())
        {
            y = amplitudeY * Mathf.Sin(omegaY * index) + 2.5f;
            minionFly.UpSide();
        }
        else
        {
            y = amplitudeY * Mathf.Sin(omegaY * index) + 42.5f;
            minionFly.DownSide();
        }
        
        transform.localPosition = new Vector3(x, y, 0);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeDamage();
            count++;
            if(count == 2)
            {
                count = 0;
                index = 0;
            }
        }
        
    }

    bool CheckHealh()
    {
        if(heal % 4 == 0 || heal % 4 == 3)
        {
            
            return true;
        }
        else
        {         
            return false;
        }

    }

}
