using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAndEnemyPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public string tag;
        public List<GameObject> elements = new List<GameObject>();
        public int sizeOfEachElement;
    }

    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> elementPooler;

	// Use this for initialization
	void Start () {
		elementPooler = new Dictionary<string, List<GameObject>>();

        foreach(Pool p in pools) {
            List<GameObject> list = new List<GameObject>();

            foreach(GameObject go in p.elements) {
                for(int i = 0 ; i < p.sizeOfEachElement ; i++) {
                    GameObject o = Instantiate(go);
                    o.SetActive(false);
                    list.Add(o);
                }
            }

            elementPooler.Add(p.tag, list);
        }
	}

    public void GetElementInPool(string tag, Vector2 position) {
        if(!elementPooler.ContainsKey(tag)) {
            Debug.LogWarning("No " + tag + " exists in the pool.");
            return;
        }

        List<GameObject> list = elementPooler[tag];
        while(true) {
            int index = Random.Range(0, list.Count);
            if(!list[index].activeInHierarchy) {
                list[index].transform.position = position;
                list[index].SetActive(true);
                break;
            }
        }
    }
}
