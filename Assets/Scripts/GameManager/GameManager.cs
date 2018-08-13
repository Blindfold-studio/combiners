using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [System.Serializable]
    public class PlayerData
    {
        public int maxHealth = 5;
        public int arrowCapacity = 5;
        public float speed = 5f;
    }

    public PlayerData playerData;

    public delegate void ResetHealthEvent();
    public static event ResetHealthEvent OnResetHealth;

    private int bossSceneCount;

    #region Player data default
    private int maxHealthDefault;
    private int arrowCapacityDefault;
    private float speedDefault;
    #endregion

    private int currentBuildIndex;
    private int nextBuildIndex;
    private int menuBuildIndex;
    private List<int> bossSceneList;

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
        bossSceneCount = SceneManager.sceneCountInBuildSettings - 1;
        bossSceneList = new List<int>();

        SetDefaultPlayerData();
        menuBuildIndex = 0;
    }
    
    public int MaxHealth
    {
        get
        {
            return playerData.maxHealth;
        }

        set
        {
            playerData.maxHealth = value;
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
            playerData.arrowCapacity = value;
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
            playerData.speed = value;
        }
    }

    public void LoadNextScene()
    {
        if (bossSceneList.Count != 0)
        {
            int index = bossSceneList[0] + 1;

            bossSceneList.RemoveAt(0);
            Time.timeScale = 1f;
            ResetHealth();
            SceneManager.LoadScene(index);
        }

        else
        {
            LoadMenuScene();
        }
        
        //SceneManager.LoadScene("PlayerScene");
    }

    public void RestartScene()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SetPlayerDataToDefault();
        ResetHealth();
        Time.timeScale = 1f;
        SceneManager.LoadScene(currentBuildIndex);
    }

    public void LoadMenuScene ()
    {
        SetPlayerDataToDefault();
        ResetHealth();
        SceneManager.LoadScene(menuBuildIndex);
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void StartGame ()
    {
        bossSceneList.Clear();

        for (int i = 0; i < bossSceneCount; i++)
        {
            int rand = Random.Range(0, bossSceneCount);
            while (bossSceneList.Contains(rand))
            {
                rand = Random.Range(0, bossSceneCount);
            }
            bossSceneList.Add(rand);
            Debug.Log(bossSceneList[i]);
        }

        LoadNextScene();
    }

    public void TestList ()
    {
        Debug.Log(bossSceneList);

        for (int i = 0; i < bossSceneCount; i++)
        {
            Debug.Log(bossSceneList[0]);
            bossSceneList.RemoveAt(0);
        }
    }

    private void ResetHealth ()
    {
        if (OnResetHealth != null)
        {
            OnResetHealth();
        }
    }

    private void SetDefaultPlayerData ()
    {
        maxHealthDefault = playerData.maxHealth;
        arrowCapacityDefault = playerData.arrowCapacity;
        speedDefault = playerData.speed;
    }

    private void SetPlayerDataToDefault ()
    {
        playerData.maxHealth = maxHealthDefault;

        playerData.arrowCapacity = arrowCapacityDefault;
        playerData.speed = speedDefault;
    }
}
