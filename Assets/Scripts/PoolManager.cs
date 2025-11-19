using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class pool
{
    public string tag;
    public GameObject prefab;
    public int count;
}

public class PoolManager : MonoBehaviour
{
    // Singleton Instance (whenever PoolManager.Instance access)
    public static PoolManager Instance;
    public List<pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Awake()
    {
        // Singleton Init
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        // Pool Init
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        
        foreach (pool pool in pools)
        {
            GameObject poolContainer = new GameObject(pool.tag + "_Pool");
            poolContainer.transform.parent = this.transform;
            
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.count; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                
                // 생성된 오브젝트 폴더안 정리
                obj.transform.parent = poolContainer.transform;
                
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        // 풀이 존재하지 않거나, 큐가 비어있을떄
        if (!poolDictionary.ContainsKey(tag) || poolDictionary[tag].Count == 0)
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        return objectToSpawn;
    }

    public void ReturnToPool(string tag, GameObject objectToReturn)
    {
        // 풀이 존재하는지 확인
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist");
            Destroy(objectToReturn); 
            return;
        }
        
        objectToReturn.SetActive(false);
        poolDictionary[tag].Enqueue(objectToReturn);
    }
}
