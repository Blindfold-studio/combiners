using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerHitBox : MonoBehaviour {

    [SerializeField]
    private float invicibleTime;

    private HealthSystem playerHP;
    private PlayerController player;

    void Start()
    {
        playerHP = HealthSystem.instance;
        player = GetComponentInParent<PlayerController>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.CompareTag("Boss") || collision.CompareTag("Enemy") || collision.CompareTag("EnemyWeapon")) && !player.IsInvicble())
        {
            Debug.Log("Player is hit by enemy");
            playerHP.HP = -1;
            player.StartCoroutine(player.Hurt(invicibleTime));
        }    

        else
        {
            return;
        }
    }
}
