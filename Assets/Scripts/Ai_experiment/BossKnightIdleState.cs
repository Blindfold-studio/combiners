﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightIdleState : State<BossKnightAI>
{
    #region initiate
    private static BossKnightIdleState instance;

    public BossKnightIdleState(BossKnightAI owner) : base (owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private float timer;
    public GameObject TargetPlayer { get; set; }

    public override void EnterState()
    {
        Debug.Log("Enter Idle state");
        timer = 0f;
        owner.Rb.velocity = Vector2.zero;
    }

    public override void ExecuteState()
    {
        timer += Time.deltaTime;
        if (timer >= owner.idleStateTime)
        {
            owner.stateMachine.ChangeState(new BossKnightMoveState(owner));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Idle state to Move state");
        owner.SetActiveShield(true);
    }

    public override void FixedUpdateExecuteState()
    {
        
    }

    public override void OnTriggerEnter()
    {
        throw new System.NotImplementedException();
    }
}
