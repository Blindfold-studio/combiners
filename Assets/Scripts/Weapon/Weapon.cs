using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    [SerializeField]
    private string weaponName;
    [SerializeField]
    private int damage;

    public virtual int Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage += value;
        }
    }
}
