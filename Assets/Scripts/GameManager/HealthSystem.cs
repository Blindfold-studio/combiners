using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthSystem : MonoBehaviour {

    # region Singleton
    public static HealthSystem instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    private int startHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int maxHealth;
    
    public Image[] healthImage;
    public Sprite[] healthSprite;

    private GameManager gameManager;

    void Start ()
    {
        gameManager = GameManager.instance;
        maxHealth = gameManager.MaxHealth;
        //currentHealth = maxHealth;
        currentHealth = startHealth;
        CheckHealth();
    }

    void CheckHealth()
    {
        for(int i = 0; i < maxHealth; i++)
        {
            if(currentHealth <= i)
            {
                healthImage[i].enabled = false;
            }
            else
            {
                healthImage[i].enabled = true;
            }
        }
        SetHealthImages();
    }
    
    void SetHealthImages()
    {
        bool empty = false;
        int i = 0;

        foreach(Image image in healthImage)
        {
            if(empty)
            {
                image.sprite = healthSprite[0];
            }
            else
            {
                i++;
                if(currentHealth >= i)
                {
                    image.sprite = healthSprite[1];
                    Debug.Log("num" + i);
                }
                else
                {
                    empty = true;
                }
            }
            Debug.Log("CUR" + currentHealth);
            Debug.Log("I" + i);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth += damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startHealth);
        SetHealthImages();
    }

    public void addHealth()
    {
        startHealth++;
        startHealth = Mathf.Clamp(startHealth, 0, gameManager.MaxHealth);
        CheckHealth();
    }

	public int CurrentHealth
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

    public int MaxHealth
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
}
