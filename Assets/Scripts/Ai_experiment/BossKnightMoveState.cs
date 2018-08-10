using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightMoveState : State<BossKnightAI>
{
    #region initiate
    private static BossKnightMoveState instance;

    public BossKnightMoveState (BossKnightAI owner) : base (owner)
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
        owner.TargetPlayer = FindTheClosestPlayer();
        Debug.Log(owner.TargetPlayer.name);
        timer = 0f;
        float distanceToPlayer = owner.TargetPlayer.transform.position.x - owner.transform.position.x;
        Flip(distanceToPlayer);
    }

    public override void ExecuteState()
    {
        timer += Time.deltaTime;
        if (timer >= owner.moveStateTime)
        {
            owner.stateMachine.ChangeState(new ThrowAxeState(owner));
        }

        float distanceToPlayer = owner.TargetPlayer.transform.position.x - owner.transform.position.x;
        //Flip(distanceToPlayer);
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
