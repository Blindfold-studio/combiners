using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {
    
    #region Singleton
    public static MissionManager instance = null;

    void Awake ()
    {
        instance = this;
    }
    #endregion

    [SerializeField]
    private Transform bossPosition_P1;
    [SerializeField]
    private Transform bossPosition_P2;

    private BossHealth bossHealth;
    private GameManager gameManager;
    private GameObject bossObject;
    private GameObject upgradePanel;
    private GameObject losePanel;

    void Start () {
        gameManager = GameManager.instance;
        
        bossObject = GameObject.FindGameObjectWithTag("Boss");
        bossHealth = bossObject.GetComponent<BossHealth>();

        upgradePanel = GameObject.FindGameObjectWithTag("WinAndUpgrade");
        losePanel = GameObject.FindGameObjectWithTag("LosePanel");
        upgradePanel.SetActive(false);
        losePanel.SetActive(false);

        Time.timeScale = 1f;
	}

    void Update()
    {
        if (bossHealth.Health <= 0)
        {
            upgradePanel.SetActive(true);
            Time.timeScale = 0f;
        }    

        if (gameManager.CurrentHealth <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public Transform GetBossPosition_P1 ()
    {
        return bossPosition_P1;
    }

    public Transform GetBossPosition_P2()
    {
        return bossPosition_P2;
    }
}
