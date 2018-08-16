using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class StraightRockState : State<GolemBoss> {

    #region initiate
    private static StraightRockState instance;

    public StraightRockState(GolemBoss owner) : base(owner)
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
        owner.anim.SetBool("IsThrowing", true);
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
        owner.anim.SetBool("IsThrowing", false);
        owner.anim.SetBool("IsIdle", true);
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
            /*
            StraightAxeBehavior straightAxe = ProjectilePool.Instance.GetElementInPool("StraightAxe", owner.transform.position, Quaternion.identity).GetComponent<StraightAxeBehavior>();
            Debug.Log(straightAxe);
            //straightAxe.SetTargetPlayer(owner.TargetPlayer);
            straightAxe.SetDirectionFromBoss(owner);
            */
            owner.anim.SetBool("IsThrowing", true);
            owner.anim.SetBool("IsIdle", false);
            canThrowAxe = false;
        }

        owner.anim.SetBool("IsThrowing", false);
        //owner.anim.SetBool("IsIdle", true);

        if (timer >= maxTime)
        {
            owner.stateMachine.ChangeState(new GolemIdleState(owner));
        }
    }
}
