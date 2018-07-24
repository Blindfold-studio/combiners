using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : EnemyManager {

    public List<Transform> items;
    private string tag;
    private ItemAndEnemyPooler itemAndEnemyPooler;

	void Start () {
        items = new List<Transform>();
        upSide = true;
        tag = "Items";
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
            //GameObject itemObject = itemAndEnemyPooler.GetElementInPool(tag, minion.position, Quaternion.identity);
            Instantiate(items[Random.Range(0, items.Count)], minion.position, Quaternion.identity);
        }
        
    }

    /*
    public void Dead()
    {
        if (heal == 0)
        {
            gameObject.SetActive(false);
            DropItem(this.transform);
        }
    }
    */
}
