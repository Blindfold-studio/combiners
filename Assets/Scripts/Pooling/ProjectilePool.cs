using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject elements;
        public int sizeOfEachElement;
        public bool canExpand;
    }

    #region Singleton

    public static ProjectilePool Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;

    Dictionary<string, Queue<GameObject>> elementDictionary;
    Dictionary<string, bool> expandDictionary;

    void Start () {
        elementDictionary = new Dictionary<string, Queue<GameObject>>();
        expandDictionary = new Dictionary<string, bool>();

        initialElementPool();
    }

    void initialElementPool()
    {
        foreach (Pool p in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < p.sizeOfEachElement; i++)
            {
                GameObject o = Instantiate(p.elements);
                o.SetActive(false);
                objectPool.Enqueue(o);
            }

            elementDictionary.Add(p.tag, objectPool);
            expandDictionary.Add(p.tag, p.canExpand);
        }
    }

    public GameObject GetElementInPool(string tag, Vector2 position, Quaternion rotation)
    {
        if (!elementDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("No " + tag + " exists in the pool.");
            return null;
        }

        if (elementDictionary[tag].Count != 0)
        {
            if (!elementDictionary[tag].Peek().activeSelf)
            {
                GameObject objectToSpawn = elementDictionary[tag].Dequeue();

                objectToSpawn.SetActive(true);
                objectToSpawn.transform.position = position;
                objectToSpawn.transform.rotation = rotation;

                IFPoolObject poolobj = objectToSpawn.GetComponent<IFPoolObject>();

                if (poolobj != null)
                {
                    poolobj.ObjectSpawn();
                }

                elementDictionary[tag].Enqueue(objectToSpawn);

                Debug.Log("Return object: " + objectToSpawn);

                return objectToSpawn;
            }

            else
            {
                if (expandDictionary[tag])
                {
                    GameObject objectToSpawn = Instantiate(elementDictionary[tag].Peek());

                    objectToSpawn.SetActive(true);
                    objectToSpawn.transform.position = position;
                    objectToSpawn.transform.rotation = rotation;

                    IFPoolObject poolobj = objectToSpawn.GetComponent<IFPoolObject>();

                    if (poolobj != null)
                    {
                        poolobj.ObjectSpawn();
                    }

                    elementDictionary[tag].Enqueue(objectToSpawn);

                    Debug.Log("Return object: " + objectToSpawn);

                    return objectToSpawn;
                }
            }
        }

        return null;
    }
}
