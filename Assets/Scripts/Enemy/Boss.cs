using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyManager {

    void Start()
    {
        heal = 5;
    }

    public virtual void TakeDamage()
    {
        heal -= 1;
    }

}
