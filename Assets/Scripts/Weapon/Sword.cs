using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    [SerializeField]
    private string weaponName = "Sword";
    [SerializeField]
    private int damage = 1;

    public int Damage
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
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossHealth>().Health = -damage;
        }

        else
        {
            return;
        }
    }
}
