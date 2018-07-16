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

    void Start () {
		
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
