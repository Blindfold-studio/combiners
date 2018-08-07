using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightMoveState : State<BossKnightAI>
{
    private static BossKnightMoveState instance;

    private BossKnightMoveState ()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static BossKnightMoveState Instance
    {
        get
        {
            if (instance == null)
            {
                new BossKnightMoveState();
            }

            return instance;
        }
    }

    public override void EnterState(BossKnightAI owner)
    {
        Debug.Log("Enter Move state");
    }

    public override void ExecuteState(BossKnightAI owner)
    {
        if (owner.switchState)
        {
            owner.stateMachine.ChangeState(BossKnightActionState.Instance);
        }
    }

    public override void ExitState(BossKnightAI owner)
    {
        Debug.Log("Exit Move state");
    }
}
