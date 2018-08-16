using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class GolemChargeState : State<GolemBoss> {

    #region initiate
    private static GolemChargeState instance;

    public GolemChargeState(GolemBoss owner) : base(owner)
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
    private float prepareToChargeTime;
    private bool canCharge;

    public override void EnterState()
    {
        Debug.Log("Enter Charge State");
        this.chargeSpeed = owner.ChargeSpeed;
        this.chargeTimeLimit = owner.ChargeTimeLimit;
        this.prepareToChargeTime = owner.PrepareToChargeTime;
        timer = 0f;
        canCharge = false;
        //owner.anim.SetBool("IsCharging", true);
    }

    public override void ExecuteState()
    {
        timer += Time.deltaTime;

        if (timer >= prepareToChargeTime)
        {            
            canCharge = true;
            timer = 0f;
            owner.anim.SetBool("IsCharging", true);
        }

        if (timer >= chargeTimeLimit || owner.transform.position.x >= owner.XMax || owner.transform.position.x <= owner.XMin)
        {
            owner.stateMachine.ChangeState(new GolemIdleState(owner));
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exit Charge State");
        owner.anim.SetBool("IsCharging", false);
    }

    public override void FixedUpdateExecuteState()
    {
        if (canCharge)
        {
            owner.Rb.velocity = owner.isFacingRight ? Vector2.right * chargeSpeed : Vector2.left * chargeSpeed;
        }
    }

    void LockTargetPlayer()
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
