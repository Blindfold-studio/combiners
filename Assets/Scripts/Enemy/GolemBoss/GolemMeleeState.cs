using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class GolemMeleeState : State<GolemBoss> {

    #region initiate
    private static GolemMeleeState instance;

    public GolemMeleeState(GolemBoss owner) : base(owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private float timer;
    private float clipLength;

    public override void EnterState()
    {
        Debug.Log("Enter ShortRangeAttack state");
        timer = 0f;
        owner.anim.SetBool("IsMelee", true);
        clipLength = owner.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public override void ExecuteState()
    {
        timer += Time.deltaTime;

        if (timer >= clipLength)
        {
            owner.stateMachine.ChangeState(new GolemIdleState(owner));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit ShortRangeAttack state");
        owner.anim.SetBool("IsMelee", false);
    }

    public override void FixedUpdateExecuteState()
    {

    }
}
