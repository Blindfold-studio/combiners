using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpeedUpBoot : MonoBehaviour {

    // private float startedTime;
    [SerializeField]
    private float durationOfBoost = 5f;
    // private float realDuration;
    [SerializeField]
    private float speed = 3f;
    private PlayerAttribute playerAttribute;
    private SpriteRenderer spriteRenderer;
    private GameObject targetPlayer;

    private void Start() {
        // realDuration = 0;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            SpeedUpBootParentP1.Instance.Speed = speed;
            SpeedUpBootParentP2.Instance.Speed = speed;
            // if(realDuration == 0 ) {
            //     startedTime = Time.time;
            // }
            // realDuration += durationOfBoost;
            if(other.gameObject.name == "Player1") {
                targetPlayer = GameObject.Find("Player2");
            } else if(other.gameObject.name == "Player2") {
               targetPlayer = GameObject.Find("Player1");
            }
            BoostPlayerSpeed();
        }
    }
    public void BoostPlayerSpeed() {
        if(targetPlayer.name == "Player1") {
            SpeedUpBootParentP1.Instance.Duration = durationOfBoost;
            SpeedUpBootParentP1.Instance.CancelBootEffectEvent += CancelPlayerBoostSpeed;
        } else if(targetPlayer.name == "Player2") {
            SpeedUpBootParentP2.Instance.Duration = durationOfBoost;
            SpeedUpBootParentP2.Instance.CancelBootEffectEvent += CancelPlayerBoostSpeed;
        }
        Color tmp = spriteRenderer.color;
        tmp.a = 0f;
        spriteRenderer.color = tmp;      
        GetComponent<BoxCollider2D> ().enabled = false;
    }

    public void CancelPlayerBoostSpeed() {
        Debug.Log("Speedup Boost lost effects.");
        if(targetPlayer.name == "Player1") {
            SpeedUpBootParentP1.Instance.CancelBootEffectEvent -= CancelPlayerBoostSpeed;
        } else if(targetPlayer.name == "Player2") {
            SpeedUpBootParentP2.Instance.CancelBootEffectEvent -= CancelPlayerBoostSpeed;
        }   
        GetComponent<BoxCollider2D> ().enabled = true;
        this.gameObject.SetActive(false);
    }

}