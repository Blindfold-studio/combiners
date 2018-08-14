using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class WizardMovement : MonoBehaviour {

    public enum State
    {
        Idle,
        Move,
        Attack
    }

    public State state;
    private MissionManager missionManager;
    private GameObject targetPlayer;
    private Transform player1_screen;
    private Transform player2_screen;
    public static event Action StopCoroutineEvent;
    private WizardAttack wizardAttackScript;

    public bool inPlayer1;
    private int randomPositionBoss;
    private int currentPosition;
    [SerializeField]
    private float teleportTimeDelay;
    [SerializeField]
    public float setCoolDownAction;
    private float coolDownAction;

    private float teleportTime;
    bool facingR;
    private float distance;
    private Vector3 curPosition;
    public GameObject portalPoint;

    #region Singleton

    public static WizardMovement Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public GameObject TargetPlayer
    {
        get
        {
            return targetPlayer;
        }

        set
        {
            targetPlayer = value;
        }
    }

    void Start()
    {
        missionManager = MissionManager.instance;
        wizardAttackScript = GetComponent<WizardAttack>();
        facingR = true;
        coolDownAction = setCoolDownAction;
        state = State.Idle;
        StartCoroutine("DelayBossSpawn");
    }

    IEnumerator DelayBossSpawn()
    {
        yield return new WaitForSeconds(0);
        curPosition = transform.position;
        CheckBossPosition();
        state = State.Move;
    }

    void Update()
    {
        TargetPlayer = FindTheClosestPlayer();
        
        BossStatus();
        
    }

    public void BossStatus()
    {
        switch (state)
        {
            case State.Idle:
                GetReady();
                break;

            case State.Move:
                Movement();
                break;

            case State.Attack:
                wizardAttackScript.AttackState();
                break;
        }
    }

    void GetReady()
    {
        Flip();
        //StartCoroutine(waitForChangeState(State.Attack, coolDownAction));
        if(coolDownAction <= 0)
        {
            state = State.Attack;
            coolDownAction = setCoolDownAction;
        }
        else
        {
            coolDownAction -= Time.deltaTime;
        }
    }
    
    /*IEnumerator waitForChangeState(State newstate, float wait)
    {
        yield return new WaitForSeconds(wait);
        state = newstate;
    }*/

    void Movement()
    {
        teleportTime -= Time.deltaTime;
        // Check where is player position.
        if (inPlayer1)
        {
            portalPoint.transform.position = new Vector3(0f, 50f, 0f);
        }
        else
        {
            portalPoint.transform.position = new Vector3(0f, 0f, 0f);
        }
        
        if(teleportTime <= 0)
        {
            randomPositionBoss = UnityEngine.Random.Range(0, portalPoint.transform.childCount);
            if(randomPositionBoss == currentPosition)
            {
                if(randomPositionBoss == portalPoint.transform.childCount - 1)
                {
                    randomPositionBoss--;
                }
                else
                {
                    randomPositionBoss++;
                }
            }
            else
            {
                currentPosition = randomPositionBoss;
            }
            teleportTime = teleportTimeDelay;
            state = State.Idle;
        }
        Flip();
        transform.position = portalPoint.transform.GetChild(randomPositionBoss).position;
    }

    void Flip()
    {
        distance = targetPlayer.transform.position.x - transform.position.x;
        if (facingR && (distance > 0))
        {

            Vector3 Scale = transform.localScale;
            if (Scale.x < 0)
            {
                Scale.x *= -1;
            }
            transform.localScale = Scale;
            facingR = false;
        }

        else if (!facingR && (distance < 0) || facingR && (distance < 0))
        {

            Vector3 Scale = transform.localScale;
            if (Scale.x > 0)
            {
                Scale.x *= -1;
            }
            transform.localScale = Scale;
            facingR = true;
        }

    }

    public GameObject FindTheClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float minDistance = Mathf.Infinity;
        GameObject targetPlayer = null;
        for (int i = 0; i < players.Length; i++)
        {
            float distance = Vector2.Distance(this.transform.position, players[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                targetPlayer = players[i];
            }
        }
        return targetPlayer;
    }

    void SwapBoss()
    {
        if (StopCoroutineEvent != null)
        {
            StopCoroutineEvent();
        }
        if (TargetPlayer.name == "Player1")
        {

            this.transform.position = missionManager.GetBossPosition_P2();

        }
        else if (TargetPlayer.name == "Player2")
        {

            this.transform.position = missionManager.GetBossPosition_P1();      
        }
        CheckBossPosition();
        TargetPlayer = FindTheClosestPlayer();
        state = State.Idle;
        wizardAttackScript.ResetAllAttack();
    }

    private float UpdatePosition()
    {
       curPosition = transform.position;
       return curPosition.y;
    }

    void CheckBossPosition()
    {
        if (UpdatePosition() >= 25)
        {
            inPlayer1 = true;
        }
        else
        {
            inPlayer1 = false;
        }
    }

    void Die()
    {
        state = State.Idle;
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
    }

    private void OnEnable()
    {
        BossHealth.SwapingEvent += SwapBoss;
        BossHealth.DeathEvent += Die;
    }

    private void OnDisable()
    {
        BossHealth.SwapingEvent -= SwapBoss;
        BossHealth.DeathEvent -= Die;
    }

}
