using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class SwappingState : State<BossKnightAI>
{
    #region initiate
    private static SwappingState instance;

    public SwappingState(BossKnightAI owner) : base(owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private float timer;
    private float swappingDuration;
    private MissionManager missionManager;

    public override void EnterState()
    {
        Debug.Log("Enter Swapping state");
        owner.Rb.velocity = Vector2.zero;
        owner.DisableHitBox();
        missionManager = MissionManager.instance;
        swappingDuration = owner.SwappingDuration;
        timer = 0f;
    }

    public override void ExecuteState()
    {
        timer += Time.deltaTime;

        if (timer >= swappingDuration)
        {
            Swapping();
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Swapping state");
        owner.EnableHitBox();
        Debug.Log("Is time to swap: " + owner.IsTimeToSwap);
    }

    public override void FixedUpdateExecuteState()
    {
        
    }

    void Swapping ()
    {
        if (owner.TargetPlayer.name == "Player1")
        {
            owner.transform.position = missionManager.GetBossPosition_P2();
        }
        else if (owner.TargetPlayer.name == "Player2")
        {
            owner.transform.position = missionManager.GetBossPosition_P1();
        }
        owner.TargetPlayer = owner.FindTheClosestPlayer();
        owner.stateMachine.ChangeState(new BossKnightMoveState(owner));
    }
}
