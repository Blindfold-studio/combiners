using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class ThrowAxeState : State<BossKnightAI> {
    #region initiate
    private static ThrowAxeState instance;

    public ThrowAxeState(BossKnightAI owner) : base (owner)
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
        
    }

    public override void ExecuteState()
    {
        
    }

    public override void FixedUpdateExecuteState()
    {
        
    }

    public override void ExitState()
    {
        
    }
}
