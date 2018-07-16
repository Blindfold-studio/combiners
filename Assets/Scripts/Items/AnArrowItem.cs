using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AnArrowItem : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if(other.gameObject.name == "Player1") {
                GameObject player = GameObject.Find("Player2");
                IncreasePlayerArrow(player);
            } else if(other.gameObject.name == "Player2") {
                GameObject player = GameObject.Find("Player1");
                IncreasePlayerArrow(player);
            }
            this.gameObject.SetActive(false);
        }
        
    }

    private void IncreasePlayerArrow(GameObject player) {
        Debug.Log(player.name + " get an arrow.");
        PlayerAttribute playerAttribute = player.GetComponent<PlayerAttribute>();
        playerAttribute.Arrow = 1;
    }
}
