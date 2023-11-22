using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleChaseOperation : MonoBehaviour
{
    private Vector3 startPos, endPos;
    //땅에 닫기까지 걸리는 시간
    protected float timer;
    protected float timeToFloor;

    private bool isCollision = false;


    public float moveSpeed = 5.0f; // 이동 속도
    public Vector3 rotationAxis = Vector3.left; // 회전 축

    Vector3 Direction;
    Quaternion targetRotation;

    public GameObject EndPosObject;

    private void Start()
    {
        startPos = transform.position;
        endPos = EndPosObject.transform.position; //startPos + new Vector3(5, 0, 0);
    }

    private void Update()
    {
        if (isCollision)
        {
            Direction = endPos - transform.position;
            targetRotation = Quaternion.LookRotation(Direction);
            transform.rotation = targetRotation;

            StartCoroutine("BulletMove");


        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
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
        while (transform.position.y >= startPos.y)
        {
            transform.rotation = Quaternion.Euler(Time.time * 900, 0, 0);

            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
            transform.position = tempPos;

           


            yield return new WaitForEndOfFrame();
        }
    }


}