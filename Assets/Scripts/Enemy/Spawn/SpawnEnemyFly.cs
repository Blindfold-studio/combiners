using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyFly : Minions {

    public GameObject enemyFly;
    private float x;
    [SerializeField]
    private float spawnTimer;
    private float spawn;
    Vector2 locate;
	// Use this for initialization

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (upSide)
        {
            if (Time.time > spawn)
            {
                spawn = spawnTimer + Time.time;
                x = Random.Range(transform.position.x - 10, transform.position.x + 10);
                locate = new Vector2(x, transform.position.y);
                Instantiate(enemyFly, locate, Quaternion.identity);
            }
        }
        else
        {
            if (Time.time > spawn)
            {
                spawn = spawnTimer + Time.time;
                x = Random.Range(transform.position.x - 10, transform.position.x + 10);
                locate = new Vector2(x, transform.position.y - 40);
                Instantiate(enemyFly, locate, Quaternion.identity);
            }
        }
        
	}

    public void UpSide()
    {
        upSide = true;
    }

    public void DownSide()
    {
        upSide = false;
    }

}
