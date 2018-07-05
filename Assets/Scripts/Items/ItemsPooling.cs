using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsPooling : MonoBehaviour {

    public GameObject applePrefab;
    public GameObject swordPrefab;

    List<GameObject> items;

    private int totalItems = 6;

	// Use this for initialization
	void Start () {
		items = new List<GameObject>();
        for(int i = 0 ; i < totalItems ; i++) {
            GameObject o;
            if(i < totalItems / 2) {
                o = (GameObject) Instantiate(applePrefab);
            } else {
                o = (GameObject) Instantiate(swordPrefab);
            }

            o.SetActive(false);
            items.Add(o);
        }
	}

    private void Update() {
        if(Input.GetMouseButtonDown(0)) {
            RandomItem(this.gameObject);
        }
    }
	
	public void RandomItem(GameObject dyingEnemy) {

        while(true) {
            int index = Random.Range(0, totalItems-1);
            if(!items[index].activeInHierarchy) {
                items[index].transform.position = dyingEnemy.transform.position;
                items[index].SetActive(true);
                break;
            }
        }
    }
}
