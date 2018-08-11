using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class ChargeState : State<BossKnightAI>
{
    #region initiate
    private static ChargeState instance;

    public ChargeState(BossKnightAI owner) : base(owner)
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }
    #endregion

    private float chargeSpeed;
    private float timer;
    private float chargeTimeLimit;

    public override void EnterState()
    {
        Debug.Log("Enter Charge State");
        this.chargeSpeed = owner.ChargeSpeed;
        this.chargeTimeLimit = owner.ChargeTimeLimit;
        timer = 0f;
        LockTargetPlayer();
    }

    public override void ExecuteState()
    {
        timer += Time.deltaTime;

        if (timer >= chargeTimeLimit || owner.transform.position.x >= owner.XMax || owner.transform.position.x <= owner.XMin)
        {
            owner.stateMachine.ChangeState(new BossKnightIdleState(owner));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Charge State");
    }

    public override void FixedUpdateExecuteState()
    {
        owner.Rb.velocity = owner.isFacingRight ? Vector2.right * chargeSpeed : Vector2.left * chargeSpeed;
    }

    void LockTargetPlayer ()
    {
        float horizontalDistance = owner.TargetPlayer.transform.position.x - owner.transform.position.x;

        if ((owner.isFacingRight && horizontalDistance <= 0) || (!owner.isFacingRight && horizontalDistance > 0))
        {
            owner.isFacingRight = !owner.isFacingRight;
            Vector3 scale = owner.transform.localScale;
            scale.x *= -1;
            owner.transform.localScale = scale;
        }
    }
}
