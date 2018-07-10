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

    public List<Pool> arrowPools;
    public List<Pool> enemyPools;
    public List<Pool> itemPools;
    public Dictionary<string, List<GameObject>> itemDictionary;
    public Dictionary<string, Queue<GameObject>> arrowDictionary;
    public Dictionary<string, bool> expandDictionary;

    // Use this for initialization
    void Start () {
        arrowDictionary = new Dictionary<string, Queue<GameObject>>();
        itemDictionary = new Dictionary<string, List<GameObject>>();
        expandDictionary = new Dictionary<string, bool>();

        initialArrowPool();
        initialItemPool();
        
	}

    void initialArrowPool ()
    {
        foreach (Pool p in arrowPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            foreach (GameObject go in p.elements)
            {
                for (int i = 0; i < p.sizeOfEachElement; i++)
                {
                    GameObject o = Instantiate(go);
                    o.SetActive(false);
                    objectPool.Enqueue(o);
                }
            }

            arrowDictionary.Add(p.tag, objectPool);
            expandDictionary.Add(p.tag, p.canExpand);
        }
    }

    void initialItemPool ()
    {
        foreach (Pool p in itemPools)
        {
            List<GameObject> list = new List<GameObject>();

            foreach (GameObject go in p.elements)
            {
                for (int i = 0; i < p.sizeOfEachElement; i++)
                {
                    GameObject o = Instantiate(go);
                    o.SetActive(false);
                    list.Add(o);
                }
            }

            itemDictionary.Add(p.tag, list);
        }
    }

    public GameObject GetArrowInPool(string tag, Vector2 position, Quaternion rotation) {
        if(!arrowDictionary.ContainsKey(tag)) {
            Debug.LogWarning("No " + tag + " exists in the pool.");
            return null;
        }

        if (arrowDictionary[tag].Count != 0)
        {
            if (!arrowDictionary[tag].Peek().activeSelf)
            {
                GameObject objectToSpawn = arrowDictionary[tag].Dequeue();

                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                arrowDictionary[tag].Enqueue(objectToSpawn);

                Debug.Log("Return object: " + objectToSpawn);

                return objectToSpawn;
            }

            else
            {
                if (expandDictionary[tag])
                {
                    GameObject objectToSpawn = Instantiate(arrowDictionary[tag].Peek());

                    objectToSpawn.SetActive(true);
                    objectToSpawn.transform.position = position;
                    objectToSpawn.transform.rotation = rotation;

                    arrowDictionary[tag].Enqueue(objectToSpawn);

                    Debug.Log("Return object: " + objectToSpawn);

                    return objectToSpawn;
                }
            }
        }

        return null;
    }

    public GameObject GetRandomItemInPool(Vector2 position, Quaternion rotation)
    {
        if (!itemDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("No " + tag + " exists in the pool.");
            return null;
        }

        List<GameObject> list = itemDictionary[tag];
        GameObject itemToSpawn = null;

        while (true)
        {
            int index = Random.Range(0, list.Count);

            itemToSpawn = list[index];

            if (!itemToSpawn.activeInHierarchy)
            {
                itemToSpawn.transform.position = position;
                itemToSpawn.SetActive(true);
                break;
            }
        }

        return itemToSpawn;
    }
}
