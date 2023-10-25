using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotCleanerMovement : MonoBehaviour
{
    enum direction { LEFT, RIGHT};

    private bool[] way =  new bool[2];


    public int Speed;
    private int angle;
    private bool rotation;

    private float maxDistance = 3;

    private int leftWay;
    private int rightWay;

    public Transform LeftLine;
    public Transform RightLine;

// true : collision
    bool isLeftHit; 
    bool isRightHit;

    private void Start()
    {
        way[(int)direction.LEFT] = false;
        way[(int)direction.RIGHT] = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (rotation)
        {
            if (way[(int)direction.LEFT] && !way[(int)direction.RIGHT])
            {
                if (angle == 0)
                {
                    angle = 360;
                }
                angle--;
            }
            else
            {
                if (angle == 360)
                {
                    angle = 0;
                }
                angle++;

            }


            if (angle % 90 == 0)
            {
                rotation = false;
            }


            transform.rotation = Quaternion.Euler(0, angle, 0);

        }
        else
        {
            if (!isLeftHit || !isRightHit)
            {
                maxDistance = 7;
            }
            else maxDistance = 3;
            transform.Translate(Vector3.right * Speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rotation = true;
    }

    private void OnDrawGizmos()
    {
        Vector3 originPosition = GameObject.Find("Position").transform.position;
        Vector3 destinationPosition;

        RaycastHit hit;
        // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
        isLeftHit = Physics.Raycast(GameObject.Find("Position").transform.position, GameObject.Find("Position").transform.forward, out hit, maxDistance);

        Gizmos.color = Color.red;
        if (isLeftHit)
        {

            way[(int)direction.LEFT] = false;
            destinationPosition = GameObject.Find("Position").transform.forward * hit.distance;
        }
        else
        {
            way[(int)direction.LEFT] = true;
            destinationPosition = GameObject.Find("Position").transform.forward * maxDistance;
        }
        Gizmos.DrawRay(originPosition, destinationPosition);

        // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
        isRightHit = Physics.Raycast(GameObject.Find("Position").transform.position, -GameObject.Find("Position").transform.forward, out hit, maxDistance);

        Gizmos.color = Color.red;
        if (isRightHit)
        {

            way[(int)direction.RIGHT] = false;
            destinationPosition = -GameObject.Find("Position").transform.forward * hit.distance;
        }
        else
        {
            way[(int)direction.RIGHT] = true;
            destinationPosition = -GameObject.Find("Position").transform.forward * maxDistance;
        }
        Gizmos.DrawRay(originPosition, destinationPosition);


    }

}
