using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance { get { return instance; } }

    public enum PoolTypes
    {
        SlimePool
    }
    [SerializeField]
    private ObjectPool[] objectPools;
    
    void Awake()
    {
        instance = this;
    }

    public GameObject GetPooledObject(PoolTypes type)
    {
        return objectPools[(int)type].GetPooledObject();
    }

    public void Reset()
    {
        foreach (ObjectPool objectPool in objectPools)
        {
            //objectPool.GameReset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
