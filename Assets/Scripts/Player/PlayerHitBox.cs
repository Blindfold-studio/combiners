using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerHitBox : MonoBehaviour {

    private HealthSystem playerHP;

    void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("GameController").GetComponent<HealthSystem>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Player is hit by enemy");
            playerHP.HP = -1;
        }    

        else
        {
            return;
        }
    }
}
