using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    public List<GameObject> objectPool;

    [SerializeField]
    private GameObject pooledObject;

    [SerializeField]
    private int poolAmount;

    public bool shouldExpand = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolAmount; i++)
        {
            CreatePooledObject();
        }
    }

    public GameObject GetPooledObject()
    {
        // loop through all the gameobjects
        for (int i = 0; i < objectPool.Count; i++)
        {
            // a gameobject is not active, then return that gameobject
            if (!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        // increase size of list if allowed
        if (shouldExpand)
        {
            GameObject obj = CreatePooledObject();
            return obj;
        }
        else
        {
            return null;
        }
    }

    private GameObject CreatePooledObject()
    {
        GameObject obj = Instantiate(pooledObject);
        obj.transform.SetParent(transform);
        obj.SetActive(false);
        objectPool.Add(obj);
        return obj;
    }
}
