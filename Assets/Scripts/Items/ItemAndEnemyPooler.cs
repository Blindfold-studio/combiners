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

        int index = Random.Range(0, list.Count);
        Debug.Log("index: " + index + " from list count: " + list.Count);

        for (int i = index; i < list.Count; i++)
        {
            GameObject objectToSpawn = list[index];

            if (!objectToSpawn.activeInHierarchy)
            {
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;
                objectToSpawn.SetActive(true);

                IFPoolObject poolobj = objectToSpawn.GetComponent<IFPoolObject>();

                if (poolobj != null)
                {
                    poolobj.ObjectSpawn();
                }

                return objectToSpawn;
            }
        }

        return null;
    }
}
