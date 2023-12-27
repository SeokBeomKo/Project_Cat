using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleChaseOperation : MonoBehaviour, IAttackable, IDamageable
{
    [Header("데이터")]
    public FlyObjectData data;
    private float height;
    private float moveSpeed = 0.5f; // 이동 속도

    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;

    private bool isCollision = false;

    private Vector3 rotationAxis = Vector3.left; // 회전 축

    Vector3 Direction;
    Quaternion targetRotation;

    [Header("아이템 리스트")]
    public List<ItemWithProbability> itemsToSpawn;

    public delegate void CapsuleHandle();
    public CapsuleHandle onFly;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        height = data.height;
        moveSpeed = data.speed;
    }

    private void Start()
    {
        startPos = transform.parent.position;
    }

    private void Update()
    {
        if (isCollision)
        {
            Direction = endPos - transform.parent.position;
            targetRotation = Quaternion.LookRotation(Direction);
            transform.parent.rotation = targetRotation;

            StartCoroutine("BulletMove");

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            CapsuleDestroy();
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.GetComponentInChildren<PlayerHitScan>().GetDamage(GetDamage());
        }

    }

    private void CapsuleDestroy()
    {
        float randomValue = UnityEngine.Random.value;
        float cumulativeProbability = 0.0f;

        foreach (var item in itemsToSpawn)
        {
            cumulativeProbability += item.probability;

            if (randomValue < cumulativeProbability)
            {
                // 아이템을 생성할 위치
                Vector3 spawnPosition = transform.parent.position;

                // 아이템 생성
                Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
                Debug.Log(item.itemPrefab.name);

                // 생성된 아이템이 있으므로 루프 종료
                break;
            }
        }

        // 캡슐 오브젝트 삭제
        transform.parent.gameObject.SetActive(false);
    }


    private void OnCollisionEnter(Collision collision)
    {
        string otherTag = collision.transform.tag;

        if (otherTag.Contains("Parts"))
        {
            onFly?.Invoke();
            isCollision = true;
        }
    }


    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);


        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    protected IEnumerator BulletMove()
    {
        isCollision = false;
        timer = 0;
        while (transform.parent.position.y >= endPos.y)
        {
            transform.parent.rotation = Quaternion.Euler(Time.time * 300, 0, 0);

            timer += Time.deltaTime * moveSpeed;
            Vector3 tempPos = Parabola(startPos, endPos, height, timer);
            transform.parent.position = tempPos;

           


            yield return new WaitForEndOfFrame();

        }
        transform.parent.gameObject.SetActive(false);
    }

    public float GetDamage()
    {
        return 5;
    }

    public void SetEndPos(Vector3 pos)
    {
        endPos = pos;
    }

    public void BeAttacked(float damage)
    {
        CapsuleDestroy();
    }
}