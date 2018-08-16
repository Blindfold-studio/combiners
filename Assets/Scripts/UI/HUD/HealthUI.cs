using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour {

    private int hp;
    private int maxHp;
    private HealthSystem healthSystem;
    private TextMeshProUGUI hpText;

    [SerializeField]
    private int startHealth;

    public Image[] healthImage;
    public Sprite[] healthSprite;

    void Start () {
        healthSystem = HealthSystem.instance;
        hpText = GetComponent<TextMeshProUGUI>();
        maxHp = healthSystem.MaxHealth;
        // hpText.text = "HP: " + hp.ToString() + "/" + maxHp.ToString();
    }
	
	void Update () {
        //hp = healthSystem.CurrentHealth;
        //maxHp = healthSystem.MaxHealth;
        // hpText.text = "HP: " + hp.ToString() + "/" + maxHp.ToString();
        CheckHealth();
    }

    void CheckHealth()
    {
        maxHp = healthSystem.MaxHealth;
        hp = healthSystem.CurrentHealth;

        for (int i = 0; i < healthImage.Length; i++)
        {
            if (i >= maxHp)
            {
                healthImage[i].gameObject.SetActive(false);
            }

            if (i < hp)
            {
                healthImage[i].sprite = healthSprite[1];
            }
            else
            {
                healthImage[i].sprite = healthSprite[0];
            }
        }
        //SetHealthImages();
    }

    void SetHealthImages()
    {
        bool empty = false;
        int i = 0;

        foreach (Image image in healthImage)
        {
            if (empty)
            {
                image.sprite = healthSprite[0];
            }
            else
            {
                i++;
                if (hp >= i)
                {
                    image.sprite = healthSprite[1];
                    Debug.Log("num" + i);
                }
                else
                {
                    empty = true;
                }
            }
            
        }
    }

   
    public void addHealth()
    {
        startHealth++;
        startHealth = Mathf.Clamp(startHealth, 0, maxHp);
        CheckHealth();
    }
}
