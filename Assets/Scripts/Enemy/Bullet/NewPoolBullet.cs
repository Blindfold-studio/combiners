using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPoolBullet : MonoBehaviour {

    public static NewPoolBullet current;
    public GameObject poolBullet;
    public int AmountOfbullets;
    public bool AllowCreate;

    public List<GameObject> poolBullets;

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        poolBullets = new List<GameObject>();
        
        
            for(int i = 0; i <AmountOfbullets; i++)
            {
                GameObject obj = (GameObject)Instantiate(poolBullet);
                obj.SetActive(false);
                poolBullets.Add(obj);
            }
            
        
    }
    public GameObject GetBullets()
    {
        for(int i = 0; i < poolBullets.Count; i++)
        {
            if (!poolBullets[i].activeInHierarchy)
            {
                return poolBullets[i];
                break;
            }
        }

        /*if (AllowCreate)
        {
            for (int i = 0; i <poolBullet.LongLength; i++)
            {
                GameObject obj = (GameObject)Instantiate(poolBullet[i]);
                poolBullets.Add(obj);
                return null;
            }
            
        }*/

        return null;
    }
}
