using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class GolemIdleState : State<GolemBoss> {

    #region initiate
    private static GolemIdleState instance;

    public GolemIdleState(GolemBoss owner) : base(owner)
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
        owner.anim.SetBool("IsIdle", true);
    }

    public override void ExecuteState()
    {
        if (owner.IsTimeToSwap && !owner.AlreadySwap)
        {
            //owner.stateMachine.ChangeState(new GolemSwappingState(owner));
        }

        timer += Time.deltaTime;
        if (timer >= owner.idleStateTime)
        {
            owner.stateMachine.ChangeState(new GolemMoveState(owner));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Idle state to Move state");
        owner.SetActiveShield(true);
        owner.anim.SetBool("IsIdle", false);
    }

    public override void FixedUpdateExecuteState()
    {

    }
}
