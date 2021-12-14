using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }

    [SerializeField] List<Pool> pools;
    [SerializeField] Dictionary<PoolType, Queue<GameObject>> poolDictionary;

    private void Awake()
    {
        Instance = this;
        SetupObjectPooler();
    }

    void SetupObjectPooler()
    {
        poolDictionary = new Dictionary<PoolType, Queue<GameObject>>();

        GameObject inst;
        foreach (var pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                inst = Instantiate(pool.prefab, transform);
                inst.SetActive(false);
                objPool.Enqueue(inst);
            }

            poolDictionary.Add(pool.type, objPool);
        }
    }

    public GameObject GetFromPool(PoolType type, Vector3 position, Quaternion rotation)
    {
        if(!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Pool with type \"{type}\" dosn`t excist.");
            return null;
        }

        GameObject objFromPool = poolDictionary[type].Dequeue();

        objFromPool.SetActive(false);
        objFromPool.transform.position = position;
        objFromPool.transform.rotation = rotation;
        objFromPool.SetActive(true);

        if (objFromPool.TryGetComponent<IPooledObject>(out IPooledObject pooledObject))
        {
            pooledObject.OnObjectSpawn();
        }

        poolDictionary[type].Enqueue(objFromPool);

        return objFromPool;
    }
}

[System.Serializable]
public class Pool
{
    public PoolType type;
    public GameObject prefab;
    public int size;
}
