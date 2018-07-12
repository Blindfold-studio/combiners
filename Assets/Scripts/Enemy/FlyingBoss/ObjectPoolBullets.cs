using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolBullets : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject bullets;
        public int size;
    }
    public static ObjectPoolBullets instance;

    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;

    private void Awake()
    {
        instance = this;    
    }
    // Use this for initialization
    void Start () {

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.bullets);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

	}
    
    public GameObject SpawnPool(string tag,Vector3 pos,Quaternion rotate)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Not exsit: " + tag);
            return null;
        }
        GameObject SpawnObject = poolDictionary[tag].Dequeue();
        SpawnObject.SetActive(true);
        SpawnObject.transform.position = pos;
        SpawnObject.transform.rotation = rotate;

        IFPoolObject poolobj = SpawnObject.GetComponent<IFPoolObject>();

        if(poolobj != null)
        {
            poolobj.ObjectSpawn();
        }

        poolDictionary[tag].Enqueue(SpawnObject);


        return SpawnObject;
        

    }
	
}
