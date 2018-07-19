using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedUpBootParentP2 : MonoBehaviour {

    private static SpeedUpBootParentP2 instance = null;
	private float duration = 0;
    private bool isOnEffect = false;
    private float speed;
    [SerializeField]
    private GameObject targetPlayer;

    public event Action CancelBootEffectEvent;

    public float Speed {
        set {
            speed = value;
        }
    }
    public float Duration{
        get {
            return duration;
        }

        set {
            duration += value;
            Debug.Log("Duration: " + duration);
            if(duration <= 0) {
                duration = 0;
            }
            if(!isOnEffect && duration > 0) {
                BoostPlayerSpeed(targetPlayer, speed);
            }
        }
    }

    static SpeedUpBootParentP2(){}

    private SpeedUpBootParentP2() {}

    public static SpeedUpBootParentP2 Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    private void Update() {
        if(duration > 0 && isOnEffect) {
            duration -= Time.deltaTime;
        } else if(duration <= 0 && isOnEffect) {
            isOnEffect = false;
            CancelPlayerBoostSpeed(targetPlayer, speed);
        }
    }

    public void BoostPlayerSpeed(GameObject player, float speed) {
        Debug.Log("Speedup Boost begins!");
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        playerAttribute.Speed = speed;
        isOnEffect = true;
    }

    public void CancelPlayerBoostSpeed(GameObject player, float speed) {
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        playerAttribute.Speed = -1*speed;
        CancelBootEffectEvent();
    }

}
