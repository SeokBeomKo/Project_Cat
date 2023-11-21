using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    // 풀에 대한 데이터를 담고 있는 클래스
    public class Pool 
    {
        public string tag;
        public GameObject prefab; 
        public int poolSize;
    }

    public List<Pool> poolList;                                    // 풀을 담당하는 리스트
    private Dictionary<string, Queue<GameObject>> poolDictionary;  // 여러개의 오브젝트 풀 관리

    private void Awake() // 풀을 담는 딕셔너리 초기화
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }


    // 새로운 풀 생성
    public void CreatePool()
    {

    }

    // 풀 
    public GameObject SpawnFromPool()
    {
        return null;
    }

    // 오브젝트를 풀로 다시 반환
    public void ReturnToPool()
    {

    }
}
