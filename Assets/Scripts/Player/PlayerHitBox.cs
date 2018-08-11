using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerHitBox : MonoBehaviour {

    [SerializeField]
    private float invicibleTime;
    [SerializeField]
    private float knockbackTime;

    private HealthSystem playerHealth;
    private PlayerController player;

    void Start()
    {
        playerHealth = HealthSystem.instance;
        player = GetComponentInParent<PlayerController>();
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("HitBox: " + collision.name);
        if ((collision.CompareTag("Boss") || collision.CompareTag("Enemy") || collision.CompareTag("EnemyWeapon")) && !player.IsInvicble())
        {
            playerHealth.CurrentHealth = -1;
            player.StartCoroutine(player.Hurt(knockbackTime ,invicibleTime));
        }    

        else
        {
            return;
        }
    }
}
