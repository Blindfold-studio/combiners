using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyHopping : Minions {

    public static SpawnEnemyHopping instance = null;

    void Awake()
    {
        instance = this;
    }
    [SerializeField]
    private Transform skelitionMinionPosition_P1;
    [SerializeField]
    private Transform skelitionMinionPosition_P2;

    public GameObject minion;
    private float x;
    [SerializeField]
    private float spawnTimer;
    private float spawn;
    Vector2 locate;
    // Use this for initialization

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(upSide);
        if (upSide)
        {
            if (Time.time > spawn)
            {
                spawn = spawnTimer + Time.time;
                x = Random.Range(transform.position.x - 10, transform.position.x + 10);
                locate = new Vector2(x, skelitionMinionPosition_P1.position.y);
                Instantiate(minion, locate, Quaternion.identity);
            }
        }
        else
        {
            if (Time.time > spawn)
            {   
                spawn = spawnTimer + Time.time;
                x = Random.Range(transform.position.x - 10, transform.position.x + 10);
                locate = new Vector2(x, skelitionMinionPosition_P2.position.y);
                Instantiate(minion, locate, Quaternion.identity);
            }
        }

    }

    public void SetSide()
    {
        if (upSide)
        {
            upSide = false;
        }
        else
        {
            upSide = true;
        }
    }



    public Transform GetMinionPosition_P1()
    {
        return skelitionMinionPosition_P1;
    }

    public Transform GetMinionPosition_P2()
    {
        return skelitionMinionPosition_P2;
    }
}
