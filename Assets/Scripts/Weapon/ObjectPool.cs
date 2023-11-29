using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    // 풀에 대한 데이터를 담고 있는 클래스
    public class Pool 
    {
        public string tag;          // 풀에 대한 고유 태그
        public GameObject prefab;   // 풀링할 게임 오브젝트
        public int poolSize;        // 풀에 저장할 오브젝트 수
    }

    public List<Pool> poolList;                                    // 풀을 담당하는 리스트
    private Dictionary<string, Queue<GameObject>> poolDictionary;  // 여러개의 오브젝트 풀 관리

    public GameObject objectPool; // 해당 풀에 부모가 될 오브젝트 (모든 풀이 같은 부모를 공유)

    private void Awake() // 풀을 담는 딕셔너리 초기화
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>(); // key, value
    }


    // 새로운 풀 생성
    public void CreatePool(string _tag, GameObject _prefab, int _size)
    {
        if(poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + "가 이미 존재.");
            return;
        }

        Pool newPool = new Pool { tag = _tag, prefab = _prefab, poolSize = _size };
        poolList.Add(newPool);

        GetFromPool(new List<Pool> { newPool });
    }

    // 풀 리스트에 풀 생성
    private void GetFromPool(List<Pool> pools) 
    {
        if (poolList.Count == 0) return;

        GameObject poolParent = new GameObject(poolList[0].tag);
        poolParent.transform.SetParent(objectPool.transform);

        // 각 풀에 대해 오브젝트 생성
        foreach(Pool pool in pools)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();

            for(int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(poolParent.transform, false);
                obj.name = pool.tag;
                objPool.Enqueue(obj); // 생성된 오브젝트를 해당 풀의 큐에 추가
            }

            // 풀 딕셔너리에 등록
            poolDictionary.Add(pool.tag, objPool);
        }

    }

    // 풀에서 오브젝트를 찾아 위치와 회전을 설정하고 활성화한 후 반환 
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + "이런 풀 없음");
            return null;
        }

        // 풀이 비어있으면 새로운 오브젝트를 생성
        if (poolDictionary[tag].Count <= 0)
        {
            Pool pool = poolList.Find(x => x.tag == tag);
            GameObject newObject = Instantiate(pool.prefab, position, rotation, transform);
            newObject.name = pool.tag;
            return newObject;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    // 오브젝트를 풀로 다시 반환
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning(tag + "이런 풀 없음");
            return;
        }

        obj.SetActive(false);

        poolDictionary[tag].Enqueue(obj);
    }
}