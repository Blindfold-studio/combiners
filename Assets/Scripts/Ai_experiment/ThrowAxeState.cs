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
    private int numOfAxe = 3;
    private int randomNumber;
    private float durationBetweenAxe = 0.5f;

    public override void EnterState()
    {
        Debug.Log("Start throwing axes");
        //randomNumber = Random.Range(0, 2);
        owner.SetActiveShield(false);
    }

    public override void ExecuteState()
    {
        randomNumber = Random.Range(0, 2);

        if (randomNumber == 0)
        {
            StraightAxeBehavior straightAxe = ProjectilePool.Instance.GetElementInPool("StraightAxe", owner.transform.position, Quaternion.identity).GetComponent<StraightAxeBehavior>();
            Debug.Log(straightAxe);
            straightAxe.SetTargetPlayer(owner.TargetPlayer);
        }

        else
        {
            ProjectileAxeBehavior projectileAxe = ProjectilePool.Instance.GetElementInPool("ProjectileAxe", owner.transform.position, Quaternion.identity).GetComponent<ProjectileAxeBehavior>();
            Debug.Log(projectileAxe);
            projectileAxe.SetTargetPlayer(owner.TargetPlayer);
            projectileAxe.AxeMoving();
        }

        owner.stateMachine.ChangeState(new BossKnightIdleState(owner));
    }

    public override void FixedUpdateExecuteState()
    {
        
    }

    public override void ExitState()
    {
        Debug.Log("Exit throwing state");
        owner.SetActiveShield(true);
    }
}
