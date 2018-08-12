using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeBox : MonoBehaviour {
    private BossKnightAI boss;
	
	void Start () {
        boss = GetComponentInParent<BossKnightAI>();
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (boss.CanMeleeAttack)
        {
            if (collision.CompareTag("Player"))
            {
                boss.stateMachine.ChangeState(new ShortRangeAttackState(boss));
            }   
        }    
    }
}
