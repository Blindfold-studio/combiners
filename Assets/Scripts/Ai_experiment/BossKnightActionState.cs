using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightActionState : State<BossKnightAI>
{
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


    public override void EnterState(BossKnightAI owner)
    {
        Debug.Log("Enter Action state");
    }

    public override void ExecuteState(BossKnightAI owner)
    {
        if (!owner.switchState)
        {
            owner.stateMachine.ChangeState(BossKnightMoveState.Instance);
        }
    }

    public override void ExitState(BossKnightAI owner)
    {
        Debug.Log("Exit Action state");
    }
}
