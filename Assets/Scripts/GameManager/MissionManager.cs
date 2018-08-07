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
    private List<Transform> bossPosition_P1;
    [SerializeField]
    private List<Transform> bossPosition_P2;

    private BossHealth bossHealth;
    private HealthSystem healthSystem;
    private SpawnEnemyFly spawnFly;
    private SpawnEnemyOnGround spawnSkeleton;
    private GameObject bossObject;
    private GameObject upgradePanel;
    private GameObject losePanel;

    void Start () {
        bossObject = GameObject.FindGameObjectWithTag("Boss");
        bossHealth = bossObject.GetComponent<BossHealth>();
        healthSystem = GetComponent<HealthSystem>();

        upgradePanel = GameObject.FindGameObjectWithTag("WinAndUpgrade");
        losePanel = GameObject.FindGameObjectWithTag("LosePanel");
        spawnFly = GetComponent<SpawnEnemyFly>();
        spawnSkeleton = GetComponent<SpawnEnemyOnGround>();
        upgradePanel.SetActive(false);
        losePanel.SetActive(false);

        InitialBossSpawn();

        Time.timeScale = 1f;
	}

    void Update()
    {
        if (bossHealth.Health <= 0)
        {
            upgradePanel.SetActive(true);
            Time.timeScale = 0f;
        }    

        if (healthSystem.CurrentHealth <= 0)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void InitialBossSpawn ()
    {
        int rand = Random.Range(1, 3);

        if (rand == 1)
        {
            bossObject.transform.position = GetBossPosition_P1();
            spawnFly.UpSide = false;
            spawnSkeleton.UpSide = false;
        }
        else
        {
            bossObject.transform.position = GetBossPosition_P2();
            spawnFly.UpSide = true;
            spawnSkeleton.UpSide = true;
        }
    }


    public Vector3 GetBossPosition_P1()
    {
        int rand = Random.Range(0, bossPosition_P1.Count);
        
        return bossPosition_P1[rand].position;
    }

    public Vector3 GetBossPosition_P2()
    {
        int rand = Random.Range(0, bossPosition_P2.Count);

        return bossPosition_P2[rand].position;
    }
}
