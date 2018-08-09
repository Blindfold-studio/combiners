using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightActionState : State<BossKnightAI>
{
    #region initiate
    private static BossKnightActionState instance;

    private BossKnightActionState()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static BossKnightActionState Instance
    {
        get
        {
            if (instance == null)
            {
                new BossKnightActionState();
            }

            return instance;
        }
    }
    #endregion

    private BossKnightAI owner;
    private GameObject targetPlayer;

    public override void EnterState(BossKnightAI owner)
    {
        Debug.Log("Enter Action state");
        this.owner = owner;
    }

    public override void ExecuteState(BossKnightAI owner)
    {
        owner.ThrowStraightAxe(owner.TargetPlayer);
        owner.stateMachine.ChangeState(BossKnightMoveState.Instance);
    }

    public override void FixedUpdateExecuteState(BossKnightAI owner)
    {
        
    }

    public override void ExitState(BossKnightAI owner)
    {
        Debug.Log("Exit Action state");
    }
}
