using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBox : MonoBehaviour {

    private GolemBoss boss;

    void Start()
    {
        boss = GetComponentInParent<GolemBoss>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (boss.CanMeleeAttack)
        {
            if (collision.CompareTag("Player"))
            {
                boss.stateMachine.ChangeState(new GolemMeleeState(boss));
            }
        }
    }
}
