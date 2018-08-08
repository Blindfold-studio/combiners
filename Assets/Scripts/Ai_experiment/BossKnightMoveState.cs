using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightMoveState : State<BossKnightAI>
{
    private BossKnightAI owner;
    private static BossKnightMoveState instance;

    public GameObject TargetPlayer { get; set; }

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

    public override void EnterState(BossKnightAI owner)
    {
        Debug.Log("Enter Move state");
        this.owner = owner;
        TargetPlayer = owner.FindTheClosestPlayer();
        Debug.Log(TargetPlayer.name);
    }

    public override void ExecuteState(BossKnightAI owner)
    {
        float distanceToPlayer = TargetPlayer.transform.position.x - owner.transform.position.x;
        owner.StartCoroutine(owner.FlipCharacter(distanceToPlayer));
        //Flip(owner, distanceToPlayer);
    }

    public override void FixedUpdateExecuteState(BossKnightAI owner)
    {
        //MoveTowardPlayer(owner);
        owner.MoveTowardPlayer();
    }

    public override void ExitState(BossKnightAI owner)
    {
        Debug.Log("Exit Move state");
    }

    public GameObject FindTheClosestPlayer(Vector3 ownerPosition)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        GameObject targetPlayer = null;
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Vector2.Distance(ownerPosition, players[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetPlayer = players[i];
            }
        }
        return targetPlayer;
    }

    //IEnumerator MoveToCharacter(BossKnightAI owner, float horizontalDistance)
    void Flip(BossKnightAI owner, float horizontalDistance)
    {
        if ((owner.isFacingRight && horizontalDistance < 0) || (!owner.isFacingRight && horizontalDistance > 0))
        {
            //onHoldForPlayerJump = true;
            owner.isFacingRight = !owner.isFacingRight;
            //yield return new WaitForSeconds(stuntAfterPlayerJumpOverHead);
            Vector3 scale = owner.transform.localScale;
            scale.x *= -1;
            owner.transform.localScale = scale;
            //onHoldForPlayerJump = false;
            //yield return null;
        }
    }

    void MoveTowardPlayer(BossKnightAI owner)
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
