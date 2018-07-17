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
        
    }

    void Update () {

        if (CheckHealh())
        {
            Controll();
        }
       
    }

    void Controll()
    {
        if (count%2==0)
        {
            index -= Time.deltaTime;
        }
        else if(count%2==1)
        {
            index += Time.deltaTime;
        }
        
        x = amplitudeX * Mathf.Cos(omegaX * index);
        if (heal % 2 == 0)
        {
            y = amplitudeY * Mathf.Sin(omegaY * index) + 3;
            minionFly.UpSide();
        }
        else
        {
            y = amplitudeY * Mathf.Sin(omegaY * index) + 13;
            minionFly.DownSide();
        }
        
        transform.localPosition = new Vector3(x, y, 0);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TakeDamage();
            index = 0;
            
        }
        
    }

    bool CheckHealh()
    {
        if(heal%2 == 0)
        {
            return true;
        }
        else
        {         
            return true;
        }

    }

}
