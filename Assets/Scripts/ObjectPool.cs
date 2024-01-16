using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance { get { return instance; } }
    public List<GameObject> pooledObjects;
    [SerializeField]
    private bool canGrow = true;
    [SerializeField]
    private GameObject pooledObject;

    [SerializeField]
    private int pooledAmount = 6;

    [SerializeField]
    private GameObject body;

    private Color color;
    private Color currentColor;


    private void Awake()
    {
        instance = this;
        color = body.GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;
        currentColor = color;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            createNewObject();
        }
    }

    private void LateUpdate()
    {
        color = body.GetComponent<SkinnedMeshRenderer>().sharedMaterial.color;
        if (currentColor != color)
        {
            SetColor();
            currentColor = color;
        }
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        if(canGrow)
        {
            createNewObject();
            Debug.Log("Ran out of available objects in pool for " + pooledObject.name + " , Growing the pool.");
            return pooledObjects[pooledObjects.Count - 1];
        }

        Debug.Log("Object pool for " + pooledObject.name + " is not big enough!");
        return null;
    }

    private void createNewObject()
    {
        GameObject newObj = Instantiate(pooledObject);
        newObj.SetActive(false);
        newObj.transform.SetParent(transform);
        pooledObjects.Add(newObj);
        newObj.GetComponent<MeshRenderer>().material.color = color;
    }

    public void SetColor()
    {
        foreach (GameObject obj in pooledObjects)
        {
            obj.GetComponent<MeshRenderer>().material.color = color;
        }
    }
}
