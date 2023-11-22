using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{
    /*bool check = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HitBox") && check == false)
        { 
            Debug.Log("충돌!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeBox"))
        {
            check = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeBox"))
        {
            check = true;
            Debug.Log("충돌X");
        }
    }*/

    [Header("최소 공격 범위 크기")]
    public float minAttackSize = 1.5f;

    [Header("최대 공격 범위 크기")]
    public float maxAttackSize = 3.5f;

    [Header("최소 안전 범위 크기")]
    public float minSafeSize = 1f;

    [Header("최대 안전 범위 크기")]
    public float maxSafeSize = 3f;

    [Header("크기 증가 최대 시간")]
    public float maxTimer = 3.5f;

    [Header("증가 속도")]
    public float growthSpeed = 1f;

    private float safeSize = 1f;
    private float attackSize = 1f;
    private float timer = 0f;

    private void Start()
    {
        SetInitialSize();
    }

    private void OnEnable()
    {
        SetInitialSize();
    }

    private void SetInitialSize()
    {
        transform.GetChild(0).localScale = new Vector3(minSafeSize, transform.GetChild(0).localScale.y, minSafeSize);
        transform.GetChild(1).localScale = new Vector3(minAttackSize, transform.GetChild(1).localScale.y, minAttackSize);

        safeSize = minSafeSize;
        attackSize = minAttackSize;
        timer = 0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer <= maxTimer)
        {
            safeSize = Mathf.Lerp(safeSize, maxSafeSize, Time.deltaTime * growthSpeed);
            transform.GetChild(0).localScale = new Vector3(safeSize, transform.GetChild(0).localScale.y, safeSize);

            attackSize = Mathf.Lerp(attackSize, maxAttackSize, Time.deltaTime * growthSpeed);
            transform.GetChild(1).localScale = new Vector3(attackSize, transform.GetChild(1).localScale.y, attackSize);
        }
    }
}