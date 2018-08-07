using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateSystem;

public class BossKnightAI : MonoBehaviour {

    [SerializeField]
    private float timeToChangeState;
    private float timer;
    private int seconds;

    public bool switchState = false;
	public StateMachine<BossKnightAI> stateMachine { get; set; }

    void Start ()
    {
        stateMachine = new StateMachine<BossKnightAI>(this);
        stateMachine.ChangeState(BossKnightMoveState.Instance);
        timer = 0f;
        seconds = 0;
    }

    void Update ()
    {
        if (Time.time > timer + 1)
        {
            timer = Time.time;
            seconds++;
            Debug.Log(seconds);
        }

        if (seconds >= timeToChangeState)
        {
            seconds = 0;
            switchState = !switchState;
        }

        stateMachine.Update();
    }
}
