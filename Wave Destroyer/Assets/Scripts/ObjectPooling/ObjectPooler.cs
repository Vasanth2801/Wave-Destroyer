using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class ObjectPool
    {
        public string objectTag;
        public GameObject prefab;
        public int size;
    }

    public ObjectPool[] pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (ObjectPool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();
            for(int i=0; i< pool.size;i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.objectTag, objPool);
        }
    }

    public GameObject SpawnFromPools(string tag, Vector3 position, Quaternion rotation)
    {
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}