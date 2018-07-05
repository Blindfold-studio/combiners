using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttribute : MonoBehaviour {

    [SerializeField]
    private float arrowCapacity;
    [SerializeField]
    private float firingRate;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float shootSpeed;

    public float ArrowCapacity
    {
        get
        {
            return arrowCapacity;
        }

        set
        {
            arrowCapacity += value;
        }
    }

    public float FiringRate
    {
        get
        {
            return firingRate;
        }

        set
        {
            firingRate = value;
        }
    }

    public float JumpPower
    {
        get
        {
            return jumpPower;
        }

        set
        {
            jumpPower += value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed += value;
        }
    }

    public float ShootSpeed
    {
        get
        {
            return shootSpeed;
        }

        set
        {
            shootSpeed += value;
        }
    }
}
