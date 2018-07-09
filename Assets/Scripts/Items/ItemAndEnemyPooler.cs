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
    //public Dictionary<string, List<GameObject>> elementPooler;
    public Dictionary<string, Queue<GameObject>> elementPooler;
    public Dictionary<string, bool> expandDictionary;

    // Use this for initialization
    void Start () {
		//elementPooler = new Dictionary<string, List<GameObject>>();
        elementPooler = new Dictionary<string, Queue<GameObject>>();
        expandDictionary = new Dictionary<string, bool>();

        foreach (Pool p in pools) {
            //List<GameObject> list = new List<GameObject>();
            Queue<GameObject> objectPool = new Queue<GameObject>();

            foreach(GameObject go in p.elements) {
                for(int i = 0 ; i < p.sizeOfEachElement ; i++) {
                    GameObject o = Instantiate(go);
                    o.SetActive(false);
                    //list.Add(o);
                    objectPool.Enqueue(o);
                }
            }

            //elementPooler.Add(p.tag, list);
            elementPooler.Add(p.tag, objectPool);
            expandDictionary.Add(p.tag, p.canExpand);
        }
	}

    public GameObject GetElementInPool(string tag, Vector2 position, Quaternion rotation) {
        if(!elementPooler.ContainsKey(tag)) {
            Debug.LogWarning("No " + tag + " exists in the pool.");
            return null;
        }

        if (elementPooler[tag].Count != 0)
        {
            if (!elementPooler[tag].Peek().activeSelf)
            {
                GameObject objectToSpawn = elementPooler[tag].Dequeue();

                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                elementPooler[tag].Enqueue(objectToSpawn);

                Debug.Log("Return object: " + objectToSpawn);

                return objectToSpawn;
            }

            else
            {
                if (expandDictionary[tag])
                {
                    GameObject objectToSpawn = Instantiate(elementPooler[tag].Peek());

                    objectToSpawn.SetActive(true);
                    objectToSpawn.transform.position = position;
                    objectToSpawn.transform.rotation = rotation;

                    elementPooler[tag].Enqueue(objectToSpawn);

                    Debug.Log("Return object: " + objectToSpawn);

                    return objectToSpawn;
                }
            }
        }

        

        /*
        List<GameObject> list = elementPooler[tag];
        foreach (GameObject go in list) {
            if(!go.activeInHierarchy) {
                go.transform.position = position;
                go.transform.rotation = rotation;
                go.SetActive(true);
                return go;
            }
        }
        */

        return null;
    }
}
