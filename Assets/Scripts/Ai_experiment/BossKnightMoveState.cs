using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightMoveState : State<BossKnightAI>
{
    #region initiate
    private static BossKnightMoveState instance;

    private BossKnightMoveState ()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public static BossKnightMoveState Instance
    {
        get
        {
            if (instance == null)
            {
                new BossKnightMoveState();
            }

            return instance;
        }
    }
    #endregion

    private BossKnightAI owner;
    private float timer;

    public override void EnterState(BossKnightAI owner)
    {
        Debug.Log("Enter Move state");
        this.owner = owner;
        owner.TargetPlayer = FindTheClosestPlayer();
        Debug.Log(owner.TargetPlayer.name);
        timer = 0f;
        float distanceToPlayer = owner.TargetPlayer.transform.position.x - owner.transform.position.x;
        Flip(distanceToPlayer);
    }

    public override void ExecuteState(BossKnightAI owner)
    {
        timer += Time.deltaTime;
        if (timer >= owner.moveStateTime)
        {
            owner.stateMachine.ChangeState(BossKnightIdleState.Instance);
        }

        float distanceToPlayer = owner.TargetPlayer.transform.position.x - owner.transform.position.x;
        //Flip(distanceToPlayer);
    }

    public override void FixedUpdateExecuteState(BossKnightAI owner)
    {
        MoveTowardPlayer();
    }

    public override void ExitState(BossKnightAI owner)
    {
        Debug.Log("Exit Move state");
        float distanceToPlayer = owner.TargetPlayer.transform.position.x - owner.transform.position.x;
        Flip(distanceToPlayer);
    }

    public GameObject FindTheClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        GameObject targetPlayer = null;
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Vector2.Distance(owner.transform.position, players[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetPlayer = players[i];
            }
        }
        return targetPlayer;
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
