﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightActionState : State<BossKnightAI>
{
    #region initiate
    public static BossKnightActionState instance;

    public BossKnightActionState(BossKnightAI owner) : base (owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private float prepareAttackTime;
    private float timer;
    private BoxCollider2D shortAttackBox;

    public override void EnterState()
    {
        Debug.Log("Enter Action state");
        shortAttackBox = owner.GetShortAttackBox();
        timer = 0f;
        prepareAttackTime = owner.PrepareAttackTime;
    }

    public override void ExecuteState()
    {
        if (Physics2D.OverlapBox(owner.TargetPlayer.transform.position, owner.TargetPlayer.transform.localScale, 0f))
        {
            owner.stateMachine.ChangeState(new ShortRangeAttackState(owner));
        }

        timer += Time.deltaTime;

        if (timer >= prepareAttackTime)
        {
            RandomAttack();
        } 
    }

    public override void FixedUpdateExecuteState()
    {
        
    }

    public override void ExitState()
    {
        Debug.Log("Exit Action state");
    }

    public override void OnTriggerEnter()
    {
        
    }

    void RandomAttack()
    {
        int rand = Random.Range(0, 3);

        if (rand == 0)
        {
            owner.stateMachine.ChangeState(new ChargeState(owner));
        }

        else if (rand == 1)
        {
            owner.stateMachine.ChangeState(new StraightAxeState(owner));
        }

        else if (rand == 2)
        {
            owner.stateMachine.ChangeState(new ProjectileAxeState(owner));
        }
    }
}
