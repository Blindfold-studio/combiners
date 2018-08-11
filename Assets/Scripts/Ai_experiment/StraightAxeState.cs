using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class StraightAxeState : State<BossKnightAI>
{
    #region initiate
    private static StraightAxeState instance;

    public StraightAxeState(BossKnightAI owner) : base(owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private GameObject targetPlayer;
    private float durationBetweenAxe = 1f;
    private float timeToThrowAxe;
    private float timer;
    private float maxTime = 3f;
    private bool canThrowAxe;

    public override void EnterState()
    {
        owner.Rb.velocity = Vector2.zero;
        Debug.Log("Start throwing axes");
        canThrowAxe = false;
        timer = 0f;
        timeToThrowAxe = durationBetweenAxe;
        owner.SetActiveShield(false);
    }

    public override void ExecuteState()
    {
        ThrowingAxe();
    }

    public override void FixedUpdateExecuteState()
    {

    }

    public override void ExitState()
    {
        Debug.Log("Exit throwing state");
        //owner.SetActiveShield(true);
    }

    void ThrowingAxe()
    {
        timer += Time.deltaTime;

        if (timer >= timeToThrowAxe)
        {
            canThrowAxe = true;
            timeToThrowAxe += durationBetweenAxe;
        }

        if (canThrowAxe)
        {
            StraightAxeBehavior straightAxe = ProjectilePool.Instance.GetElementInPool("StraightAxe", owner.transform.position, Quaternion.identity).GetComponent<StraightAxeBehavior>();
            Debug.Log(straightAxe);
            straightAxe.SetTargetPlayer(owner.TargetPlayer);
            canThrowAxe = false;
        }

        if (timer >= maxTime)
        {
            owner.stateMachine.ChangeState(new BossKnightIdleState(owner));
        }
    }

    public override void OnTriggerEnter()
    {

    }
}
