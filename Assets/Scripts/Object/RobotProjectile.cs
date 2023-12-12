using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotProjectile : MonoBehaviour
{
    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;

    private bool isCollision = false;

    public float height = 1;
    private float moveSpeed = 0.1f; // 이동 속도
    public Vector3 rotationAxis = Vector3.left; // 회전 축

    Vector3 Direction;
    Quaternion targetRotation;

    private void Start()
    {
        isCollision = true;
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
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (null != other.transform.GetComponentInChildren<PlayerHitScan>())
            {
                other.transform.GetComponentInChildren<PlayerHitScan>().GetDamage();
            }
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
            transform.parent.rotation = Quaternion.Euler(Time.time * 90, 0, 0);

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

    public void SetEndPos(Vector3 start, Vector3 end)
    {
        startPos = start;
        endPos = end;
    }
}
