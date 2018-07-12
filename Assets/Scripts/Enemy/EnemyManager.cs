using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int  heal = 1;
	
    public virtual void TakeDamage()
    {
        heal -= 1;
    }

}
