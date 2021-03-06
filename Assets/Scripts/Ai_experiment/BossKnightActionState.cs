﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightActionState : State<BossKnightAI>
{
    #region initiate
    private static BossKnightActionState instance;

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
        owner.CanMeleeAttack = true;
    }

    public override void ExecuteState()
    {
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
        owner.CanMeleeAttack = false;
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
