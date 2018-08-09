using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightIdleState : State<BossKnightAI>
{
    #region initiate
    private static BossKnightIdleState instance;

    public GameObject TargetPlayer { get; set; }

    private BossKnightIdleState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static BossKnightIdleState Instance
    {
        get
        {
            if (instance == null)
            {
                new BossKnightIdleState();
            }

            return instance;
        }
    }
    #endregion

    private float timer;

    public override void EnterState(BossKnightAI owner)
    {
        Debug.Log("Enter Idle state");
        timer = 0f;
    }

    public override void ExecuteState(BossKnightAI owner)
    {
        timer += Time.deltaTime;
        if (timer >= owner.idleStateTime)
        {
            owner.stateMachine.ChangeState(BossKnightActionState.Instance);
        }
    }

    public override void ExitState(BossKnightAI owner)
    {
        Debug.Log("Exit Idle state to Action state");
    }

    public override void FixedUpdateExecuteState(BossKnightAI owner)
    {
        
    }
}
