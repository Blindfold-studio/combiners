using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class GolemMoveState : State<GolemBoss> {

    #region initiate
    private static GolemMoveState instance;

    public GolemMoveState(GolemBoss owner) : base(owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private float timer;

    public override void EnterState()
    {
        Debug.Log("Enter Move state");
        //owner.TargetPlayer = owner.FindTheClosestPlayer();
        Debug.Log(owner.TargetPlayer.name);
        timer = 0f;
        float distanceToPlayer = owner.TargetPlayer.transform.position.x - owner.transform.position.x;
        Flip(distanceToPlayer);
    }

    public override void ExecuteState()
    {
        if (owner.IsTimeToSwap && !owner.AlreadySwap)
        {
            //owner.stateMachine.ChangeState(new SwappingState(owner));
        }

        timer += Time.deltaTime;
        if (timer >= owner.moveStateTime)
        {
            owner.stateMachine.ChangeState(new GolemActionState(owner));
        }
    }

    public override void FixedUpdateExecuteState()
    {
        MoveTowardPlayer();
    }

    public override void ExitState()
    {
        Debug.Log("Exit Move state");
        float distanceToPlayer = owner.TargetPlayer.transform.position.x - owner.transform.position.x;
        Flip(distanceToPlayer);
    }

    void Flip(float horizontalDistance)
    {
        if ((owner.isFacingRight && horizontalDistance < 0) || (!owner.isFacingRight && horizontalDistance > 0))
        {
            owner.isFacingRight = !owner.isFacingRight;
            Vector3 scale = owner.transform.localScale;
            scale.x *= -1;
            owner.transform.localScale = scale;
        }
    }

    void MoveTowardPlayer()
    {
        float vel = owner.Speed;

        if (!owner.isFacingRight && owner.Speed > 0)
        {
            vel *= -1;
        }
        else if (owner.isFacingRight && owner.Speed < 0)
        {
            vel *= -1;
        }
        owner.Rb.velocity = new Vector2(vel, owner.Rb.velocity.y);
    }

}
