﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerAttribute : MonoBehaviour {
    public TextMeshProUGUI arrowText;
    //public TextMeshProUGUI speedText;

    [SerializeField]
    private int arrowCapacity;
    [SerializeField]
    private float firingRate;
    [SerializeField]
    private float jumpPower;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float shootSpeed;

    private int currentArrow;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;

        arrowCapacity = gameManager.MaxArrow;
        speed = gameManager.Speed;

        currentArrow = arrowCapacity;

        //arrowText.text = "Arrow: " + currentArrow.ToString() + "/" + gameManager.MaxArrow.ToString();
        //speedText.text = "Speed: " + speed.ToString() + "/" + gameManager.Speed.ToString();
    }

    void Update ()
    {
        //arrowText.text = "Arrow: " + currentArrow.ToString() + "/" + gameManager.MaxArrow.ToString();
        //speedText.text = "Speed: " + speed.ToString() + "/" + gameManager.Speed.ToString();
    }

    public int Arrow
    {
        get
        {
            return currentArrow;
        }

        set
        {
            currentArrow += value;

            if (currentArrow >= arrowCapacity)
            {
                currentArrow = arrowCapacity;
            }
            else if (currentArrow <= 0)
            {
                currentArrow = 0; 
            }
        }
    }

    public int MaxArrow
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

            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
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
