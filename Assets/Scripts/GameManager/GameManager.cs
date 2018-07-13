using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [System.Serializable]
    public class PlayerData
    {
        public int maxHealth = 5;
        public int arrowCapacity = 5;
        public float speed = 5f;
    }

    public PlayerData playerData;

    private HealthSystem healthSystem;

    #region Singleton Object
    public static GameManager instance = null;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }
    
    public int MaxHealth
    {
        get
        {
            return playerData.maxHealth;
        }

        set
        {
            playerData.maxHealth += value;
            healthSystem.MaxHP = playerData.maxHealth;
        }
    }

    public int MaxArrow
    {
        get
        {
            return playerData.arrowCapacity;
        }

        set
        {
            playerData.arrowCapacity += value;
        }
    }

    public float Speed
    {
        get
        {
            return playerData.speed;
        }

        set
        {
            playerData.speed += value;
        }
    }
}
