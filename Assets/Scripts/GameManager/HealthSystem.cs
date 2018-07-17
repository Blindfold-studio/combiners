using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int maxHealth;

    private GameManager gameManager;

    void Start ()
    {
        gameManager = GetComponent<GameManager>();

        maxHealth = gameManager.MaxHealth;
        currentHealth = maxHealth;
    }

	public int HP
    {
        get
        {
            return currentHealth;
        }

        set
        {
            currentHealth += value;

            if (currentHealth >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else if (currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
    }

    public int MaxHP
    {
        get
        {
            return maxHealth;
        }

        set
        {
            maxHealth = value;
        }
    }

    private void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        GameManager.OnResetHealth += ResetHealth;
    }

    private void OnDisable()
    {
        GameManager.OnResetHealth -= ResetHealth;
    }
}
