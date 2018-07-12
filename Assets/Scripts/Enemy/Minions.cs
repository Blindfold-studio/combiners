using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : EnemyManager {

	
	void Start () {
        heal = 1;
	}

    public virtual void TakeDamage()
    {
        heal -= 1;
    }

}
