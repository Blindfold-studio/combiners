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

    public override void EnterState(BossKnightAI owner)
    {
        Debug.Log("Enter Action state");
    }

    public override void ExecuteState(BossKnightAI owner)
    {

    }

    public override void FixedUpdateExecuteState(BossKnightAI owner)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(BossKnightAI owner)
    {
        Debug.Log("Exit Action state");
    }
}
