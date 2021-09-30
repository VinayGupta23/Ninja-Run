using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    private List<GameObject> objPool;

    public int poolSize = 10;
    public GameObject pooledObject;

    private GameObject addNewToPool()
    {
        GameObject obj = Instantiate(pooledObject) as GameObject;
        obj.SetActive(false);
        objPool.Add(obj);
        return obj;
    }

    void Awake ()
    {
        objPool = new List<GameObject>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            addNewToPool();
        }
	}
	
	public GameObject GetUnused()
    {
        for (int i = 0; i < objPool.Count; i++)
        {
            if (objPool[i].activeInHierarchy == false)
                return objPool[i];
        }

        GameObject obj = addNewToPool();
        return obj;
    }
}
