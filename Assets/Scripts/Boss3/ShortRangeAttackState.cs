using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class ShortRangeAttackState : State<BossKnightAI>
{
    #region initiate
    private static ShortRangeAttackState instance;

    public ShortRangeAttackState(BossKnightAI owner) : base(owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private float timer;
    private float shortAttackDuration;

    public override void EnterState()
    {
        Debug.Log("Enter ShortRangeAttack state");
        shortAttackDuration = owner.ShortAttackDuration;
        timer = 0f;
        owner.SetActiveShield(false);
        owner.MeleeAttack();
    }

    public override void ExecuteState()
    {
        timer += Time.deltaTime;

        if (timer >= shortAttackDuration)
        {
            owner.stateMachine.ChangeState(new BossKnightIdleState(owner));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit ShortRangeAttack state");
        owner.StopMeleeAttack();
    }

    public override void FixedUpdateExecuteState()
    {
        
    }
}
