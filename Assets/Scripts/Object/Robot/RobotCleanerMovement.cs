using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotCleanerMovement : MonoBehaviour
{
    public Vector3 PlayerPos;

    public float Speed;
    public bool isCollision = false;
    private bool isMoving = true;
    public int rotateDirection = 1;
    public int angle = 90;

    public delegate void RobotHandle();
    public event RobotHandle onMove;

    private void OnEnable()
    {
        isCollision = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            isMoving = false;
            GetComponent<RobotAttack>().enabled = true;
            GetComponent<RobotCleanerMovement>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        onMove?.Invoke();

        Debug.Log("robot collision : " + collision.gameObject.tag);
        isCollision = true;

        Vector3 direction = PlayerPos - transform.position;
        Vector3 cross = Vector3.Cross(direction, transform.right);
        float dot = Vector3.Dot(cross, transform.up);

        if (dot > 0)
        {
            // left
            rotateDirection = 1;
        }
        else
        {
            rotateDirection = -1;
        }



    }

    void Update()
    {
        if (isMoving)
        {
            if (isCollision)
            {
                if (rotateDirection > 0)
                {
                    TurnLeft();
                }
                else
                {
                    TurnRight();

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

}
