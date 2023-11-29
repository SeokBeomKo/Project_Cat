using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantChaseOperation : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float moveSpeed = 2f;

    private bool isRolling = false;
    private float angle;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.CompareTag("ChaseRoad"))
        {
            Debug.Log("바닥이랑 충돌");

            isRolling = true;
        }
    }

    private void Update()
    {
        if(isRolling)
        {
            PlantRoll();
        }
    }

    private void PlantRoll()
    {
        angle++;
        if(angle==360)
        {
            angle = 0;
        }
        //Debug.Log("angle : " + angle);
        //transform.parent.rotation = Quaternion.Euler(0, angle, 0);
        //transform.parent.position += new Vector3(0, 0, -1f) * Time.deltaTime;

       
        // y축을 중심으로 회전
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // 플레이어 방향으로 이동
        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

}
