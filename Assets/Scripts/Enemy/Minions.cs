using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : EnemyManager {

    public List<Transform> items;
    private ItemAndEnemyPooler itemAndEnemyPooler;

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
            int rand = Random.Range(0, items.Count);
            Debug.Log(rand);
            //GameObject itemObject = itemAndEnemyPooler.GetElementInPool("Items", minion.position, Quaternion.identity);
            Instantiate(items[rand], minion.position, Quaternion.identity);
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
