using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttribute : MonoBehaviour {
    public TextMeshProUGUI arrowText;
    public TextMeshProUGUI speedText;

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

    private float curruntArrow;

    void Start()
    {
        curruntArrow = arrowCapacity;

        arrowText.text = "Arrow: " + curruntArrow.ToString() + "/" + arrowCapacity.ToString();
        speedText.text = "Speed: " + speed.ToString();
    }

    void Update ()
    {
        arrowText.text = "Arrow: " + curruntArrow.ToString() + "/" + arrowCapacity.ToString();
        speedText.text = "Speed: " + speed.ToString();
    }

    public float Arrow
    {
        get
        {
            return curruntArrow;
        }

        set
        {
            curruntArrow += value;
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
