using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : EnemyManager {

    void Start()
    {
        upSide = false;
    }

    public virtual void TakeDamage()
    {
        heal -= 1;
    }

}
