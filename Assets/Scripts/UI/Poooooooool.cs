using System.Collections.Generic;
using UnityEngine;
using System;

public class Poooooooool : Singleton<Poooooooool>
{
    // 풀 정보를 포함하는 클래스
    [Serializable]
    public class Pool
    {
        public string tag;          // 각 풀에 대한 고유 태그
        public GameObject prefab;   // 풀링할 게임 오브젝트 prefab
        public int size;            // 풀에 저장할 게임 오브젝트 수

        public bool isUI;           // UI 라면 캔버스 풀에 생성
    }

    [SerializeField] private List<Pool> pools;                      // 풀 목록
    private Dictionary<string, Queue<GameObject>> poolDictionary;   // 풀을 저장하기 위한 딕셔너리

    [SerializeField] private GameObject objPool;
    [SerializeField] private GameObject objPool_UI;

    void Awake()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        //CreatePools(pools);
    }

    /// <summary>
    /// 주어진 매개변수를 사용하여 풀(Pool)을 생성하고, 해당 풀에 게임 오브젝트들을 추가합니다.
    /// </summary>
    /// <param name="_tag">게임 오브젝트를 구분하는 태그입니다.</param>
    /// <param name="_prefab">생성할 게임 오브젝트의 프리팹입니다.</param>
    /// <param name="_size">풀에 저장할 게임 오브젝트의 수입니다.</param>
    /// <param name="_isUI">UI 인지 아닌지 판별하는 변수입니다.</param>
    
    public void AddPool(string _tag, GameObject _prefab, int _size, bool _isUI = false)
    {
        if (poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("A pool with the tag: " + tag + " is already present.");
            return;
        }

        // 새로운 Pool 객체를 생성합니다.
        Pool newPool = new Pool { tag = _tag, prefab = _prefab, size = _size, isUI = _isUI };
        pools.Add(newPool);

        CreatePools(new List<Pool> { newPool });
    }

    // 풀 목록에서 풀을 생성하는 함수
    private void CreatePools(List<Pool> poolList)
    {
        if (poolList.Count == 0)
        {
            return;
        }
        GameObject poolParent = new GameObject(poolList[0].tag);

        if (poolList[0].isUI)
        {
            poolParent.transform.SetParent(objPool_UI.transform, false);
        }
        else
        {
            poolParent.transform.SetParent(objPool.transform);
        }

        foreach (Pool pool in poolList)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            // 설정한 사이즈만큼 게임 오브젝트를 생성하여 큐에 넣는다
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(poolParent.transform, false);  //부모 오브젝트가 될 풀의 자식으로 설정합니다.
                obj.name = pool.tag; // 이름으로 태그 사용
                objectPool.Enqueue(obj);
            }

            // 태그를 키로 사용하여 딕셔너리에 풀을 추가한다
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    // 풀에서 오브젝트를 찾아 위치와 회전을 설정하고 활성화한 후 반환한다
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("No pool with the tag: " + tag);
            return null;
        }

        // 큐에 오브젝트가 없으면 풀의 사이즈를 증가
        if (poolDictionary[tag].Count <= 0)
        {
            Pool pool = pools.Find(x => x.tag == tag);
            GameObject newObj = Instantiate(pool.prefab, position, rotation, transform);
            newObj.SetActive(true);
            newObj.name = pool.tag;
            return newObj;
        } 

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        objectToSpawn.SetActive(true);

        return objectToSpawn;
    }

    // 오브젝트를 다시 풀로 리턴하는 함수
    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag)) // Dictionary에 키를 추가하는 경우 키가 중복하는지 확인
        {
            Debug.LogWarning("No pool with the tag: " + tag);
            return;
        }

        // 오브젝트를 비활성화한다
        obj.SetActive(false);

        // 풀에 다시 반환한다
        poolDictionary[tag].Enqueue(obj);
    }
}