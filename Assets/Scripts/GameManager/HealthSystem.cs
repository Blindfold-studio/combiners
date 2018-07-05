using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField]
    private int currentHP;
    [SerializeField]
    private int maxHP;

    void Start ()
    {
        currentHP = maxHP;
    }

	public int HP
    {
        get
        {
            return currentHP;
        }

        set
        {
            currentHP += value;
        }
    }

    public int MaxHP
    {
        get
        {
            return maxHP;
        }

        set
        {
            maxHP += value;
        }
    }
}
