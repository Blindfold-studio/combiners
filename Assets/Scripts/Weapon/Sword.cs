using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {

    [SerializeField]
    private string weaponName = "Sword";
    [SerializeField]
    private int damage = 1;

    public override int Damage
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Hit Enemy
        }

        else
        {
            return;
        }
    }
}
