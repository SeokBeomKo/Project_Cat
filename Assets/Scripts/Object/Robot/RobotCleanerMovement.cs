using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotCleanerMovement : MonoBehaviour
{
    public Transform playerTransform;
    public int Speed;

    public bool isMoving = false;
    public bool isCollision = false;
    public int rotateDirection = 1;
    public int angle = 90;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Finish"))
        {

        }
        else
        {
            Debug.Log("collision");
            isCollision = true;

            Vector3 direction = transform.position - playerTransform.position;
            Vector3 cross = Vector3.Cross(direction, transform.forward);
            float dot = Vector3.Dot(cross, transform.up);

            if (dot > 0)
            {
                rotateDirection = 1;
            }
            else
            {
                rotateDirection = -1;
            }
        }
    }

    void Update()
    {
        if (isMoving)
        {
            if (isCollision)
            {
                if(rotateDirection > 0)
                {
                    Debug.Log("right turn");
                    if (angle == 360)
                    {
                        angle = 0;
                    }
                    angle++;
                }
                else
                {
                    Debug.Log("left turn");
                    if (angle == 0)
                    {
                        angle = 360;
                    }
                    angle--;
                }
                transform.parent.rotation = Quaternion.Euler(0, angle, 0);

                if (angle % 90 == 0)
                {
                    isCollision = false;
                }
            }
            else
            {
                transform.parent.Translate(Vector3.forward * Speed * Time.deltaTime);
            }
        }
    }

    void HandleCollision()
    {
        //왼쪽 양
        // 로봇청소기와 플레이어 간의 X 좌표를 비교하여 왼쪽 또는 오른쪽으로 회전
        Vector3 direction = transform.position - playerTransform.position;
        Vector3 cross = Vector3.Cross(direction, transform.forward);
        float dot = Vector3.Dot(cross, transform.up);

        Debug.Log("Dot : " + dot);
        //float robotX = transform.position.x;
        //float playerX = playerTransform.position.x;

        if (dot > 0)
        {

            Debug.Log("right turn");
            TurnRight(); // 로봇청소기가 플레이어의 왼쪽에 있으면 오른쪽으로 회전
        }
        else
        {
            Debug.Log("left turn");
            TurnLeft(); // 로봇청소기가 플레이어의 오른쪽에 있으면 왼쪽으로 회전
        }

        if (angle % 90 == 0)
        {
            isCollision = false;
        }

        transform.parent.rotation = Quaternion.Euler(0, angle, 0);

    }

    void TurnLeft()
    {
        if (angle == 0)
        {
            angle = 360;
        }
        angle--;

    }

    void TurnRight()
    {
        if (angle == 360)
        {
            angle = 0;
        }
        angle++;
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    //private bool isMoving = false;

    //enum direction { LEFT, RIGHT};

    //private bool[] way =  new bool[2];


    //public int Speed;
    //private int angle;
    //private bool rotation;

    //private float maxDistance = 3;

    //private int leftWay;
    //private int rightWay;

    //public Transform LeftLine;
    //public Transform RightLine;

    //// true : collision
    //bool isLeftHit; 
    //bool isRightHit;

    //private void Start()
    //{
    //    way[(int)direction.LEFT] = false;
    //    way[(int)direction.RIGHT] = false;
    //}
    //// Update is called once per frame
    //void Update()
    //{

    //    if (rotation)
    //    {
    //        if (way[(int)direction.LEFT] && !way[(int)direction.RIGHT])
    //        {
    //            if (angle == 0)
    //            {
    //                angle = 360;
    //            }
    //            angle--;
    //        }
    //        else
    //        {
    //            if (angle == 360)
    //            {
    //                angle = 0;
    //            }
    //            angle++;

    //        }


    //        if (angle % 90 == 0)
    //        {
    //            rotation = false;
    //        }


    //        transform.rotation = Quaternion.Euler(0, angle, 0);

    //    }
    //    else
    //    {
    //        if (!isLeftHit || !isRightHit)
    //        {
    //            maxDistance = 7;
    //        }
    //        else maxDistance = 3;
    //        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    rotation = true;
    //}

    //private void OnDrawGizmos()
    //{
    //    Vector3 originPosition = GameObject.Find("Position").transform.position;
    //    Vector3 destinationPosition;

    //    RaycastHit hit;
    //    // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
    //    isLeftHit = Physics.Raycast(GameObject.Find("Position").transform.position, GameObject.Find("Position").transform.forward, out hit, maxDistance);

    //    Gizmos.color = Color.red;
    //    if (isLeftHit)
    //    {

    //        way[(int)direction.LEFT] = false;
    //        destinationPosition = GameObject.Find("Position").transform.forward * hit.distance;
    //    }
    //    else
    //    {
    //        way[(int)direction.LEFT] = true;
    //        destinationPosition = GameObject.Find("Position").transform.forward * maxDistance;
    //    }
    //    Gizmos.DrawRay(originPosition, destinationPosition);

    //    // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
    //    isRightHit = Physics.Raycast(GameObject.Find("Position").transform.position, -GameObject.Find("Position").transform.forward, out hit, maxDistance);

    //    Gizmos.color = Color.red;
    //    if (isRightHit)
    //    {

    //        way[(int)direction.RIGHT] = false;
    //        destinationPosition = -GameObject.Find("Position").transform.forward * hit.distance;
    //    }
    //    else
    //    {
    //        way[(int)direction.RIGHT] = true;
    //        destinationPosition = -GameObject.Find("Position").transform.forward * maxDistance;
    //    }
    //    Gizmos.DrawRay(originPosition, destinationPosition);


    //}

   
}
