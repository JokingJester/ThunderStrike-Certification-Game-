using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private static PoolManager _instance;
    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("Pool Manager Is Null");
            return _instance;
        }
    }

    public List<Pool> masterPool;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        //generates 3 prefabs in each object pool lists
        for (int i = 0; i < masterPool.Count; i++)
        {
            GeneratePrefabs(3, i);
        }
    }
    List<GameObject> GeneratePrefabs(int amountOfPrefabs, int id)
    {
        for (int i = 0; i < amountOfPrefabs; i++)
        {
            GameObject prefab = Instantiate(masterPool[id].prefab);
            prefab.transform.parent = masterPool[id].prefabHolder.transform;
            prefab.SetActive(false);
            masterPool[id].poolList.Add(prefab);
        }
        return masterPool[id].poolList;
    }

    public GameObject RequestPrefab(Vector3 requestedPos, int id)
    {
        foreach(var gObject in masterPool[id].poolList)
        {
            if(gObject.activeInHierarchy == false)
            {
                gObject.transform.position = requestedPos;
                gObject.SetActive(true);
                return gObject;
            }
        }

        GameObject newPrefab = Instantiate(masterPool[id].prefab);
        newPrefab.transform.parent = masterPool[id].prefabHolder.transform;
        masterPool[id].poolList.Add(newPrefab);
        newPrefab.transform.position = requestedPos;
        return newPrefab;

    }
}
