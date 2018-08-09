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

    private GameObject targetPlayer;

    public override void EnterState()
    {
        Debug.Log("Enter Action state");
    }

    public override void ExecuteState()
    {
        owner.ThrowStraightAxe(owner.TargetPlayer);
        owner.stateMachine.ChangeState(new BossKnightMoveState(owner));
    }

    public override void FixedUpdateExecuteState()
    {
        
    }

    public override void ExitState()
    {
        Debug.Log("Exit Action state");
    }
}
