using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : EnemyManager {
    private ItemAndEnemyPooler itemAndEnemyPooler;
   

	void Start () {
        itemAndEnemyPooler = ItemAndEnemyPooler.Instance;
        
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
            GameObject itemObject = itemAndEnemyPooler.GetElementInPool("Items", minion.position, Quaternion.identity);
        }
        
    }

    public void Dead()
    {
        if (heal == 0)
        {
            gameObject.SetActive(false);
            DropItem(this.transform);
            
        }
    }
}
