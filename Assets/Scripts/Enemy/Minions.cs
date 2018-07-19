using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : EnemyManager {

    public List<Transform> items;

	void Start () {
        items = new List<Transform>();
        upSide = true;
	}

    public virtual void TakeDamage()
    {
        heal -= 1;
    }

    public void DropItem(Transform minion)
    {
        var DropingRate = Random.Range(0,5);
        if (DropingRate == 1)
        {
            Instantiate(items[Random.Range(0, items.Count)], minion.position, Quaternion.identity);
        }
        
    }

    
}
