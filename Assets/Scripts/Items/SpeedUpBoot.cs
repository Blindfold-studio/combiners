using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpeedUpBoot : MonoBehaviour {

    private float startedTime;
    [SerializeField]
    private float durationOfBoost = 5f;
    private float realDuration;
    [SerializeField]
    private float speed = 3f;
    private PlayerAttribute playerAttribute;
    private SpriteRenderer spriteRenderer;

    private void Start() {
        realDuration = 0;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if(realDuration == 0 ) {
                startedTime = Time.time;
            }
            realDuration += durationOfBoost;
            if(other.gameObject.name == "Player1") {
                GameObject o = GameObject.Find("Player2");
                BoostPlayerSpeed(o);
            } else if(other.gameObject.name == "Player2") {
                GameObject o = GameObject.Find("Player1");
                BoostPlayerSpeed(o);
            }
        }
    }

    private void Update() {
        if(Time.time - startedTime > realDuration && startedTime != 0) {
            CancelPlayBoostSpeed();
        }
    }

    public void BoostPlayerSpeed(GameObject player) {
        Debug.Log("Speedup Boost begins!");
        playerAttribute = player.GetComponent<PlayerAttribute>();
        playerAttribute.Speed = speed;
        Color tmp = spriteRenderer.color;
        tmp.a = 0f;
        spriteRenderer.color = tmp;      
        GetComponent<BoxCollider2D> ().enabled = false;
    }

    public void CancelPlayBoostSpeed() {
        realDuration = 0;
        Debug.Log("Speedup Boost lost effects.");
        playerAttribute.Speed = -1*speed;
        GetComponent<BoxCollider2D> ().enabled = true;
        this.gameObject.SetActive(false);
    }

}