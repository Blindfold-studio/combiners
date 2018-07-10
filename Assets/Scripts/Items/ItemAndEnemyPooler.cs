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

    public List<Pool> elementPools;

    Dictionary<string, List<GameObject>> elementDictionary;
    Dictionary<string, bool> expandDictionary;

    void Start () {
        elementDictionary = new Dictionary<string, List<GameObject>>();
        expandDictionary = new Dictionary<string, bool>();

        initialElementPool();
	}

    void initialElementPool ()
    {
        foreach (Pool p in elementPools)
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

            elementDictionary.Add(p.tag, list);
        }
    }

    public GameObject GetElementInPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!elementDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("No " + tag + " exists in the pool.");
            return null;
        }

        List<GameObject> list = elementDictionary[tag];
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
