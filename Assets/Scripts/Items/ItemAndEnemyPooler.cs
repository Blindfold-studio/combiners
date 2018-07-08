using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAndEnemyPooler : MonoBehaviour {

    [System.Serializable]
    public class Pool {
        public string tag;
        public List<GameObject> elements = new List<GameObject>();
        public int sizeOfEachElement;
        public bool canExpand;
    }

    #region Singleton

    public static ItemAndEnemyPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

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

    public GameObject GetElementInPool(string tag, Vector2 position, Quaternion rotation) {
        if(!elementPooler.ContainsKey(tag)) {
            Debug.LogWarning("No " + tag + " exists in the pool.");
            return null;
        }

        List<GameObject> list = elementPooler[tag];
        foreach (GameObject go in list) {
            if(!go.activeInHierarchy) {
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);
                return go;
            }
        }

        Debug.Log("Not found");
        return null;
    }
}
