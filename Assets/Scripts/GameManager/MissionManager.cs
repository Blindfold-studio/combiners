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

    public GameObject BossObject;

    [SerializeField]
    private Transform bossPosition_P1;
    [SerializeField]
    private Transform bossPosition_P2;

    private BossHealth bossHealth;
    private GameObject upgradePanel;

    void Start () {
        bossHealth = BossObject.GetComponent<BossHealth>();
        upgradePanel = GameObject.FindGameObjectWithTag("WinAndUpgrade");
        upgradePanel.SetActive(false);
	}

    void Update()
    {
        if (bossHealth.Health <= 0)
        {
            upgradePanel.SetActive(true);
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
