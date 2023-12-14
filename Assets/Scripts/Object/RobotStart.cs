using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStart : MonoBehaviour
{

    public delegate void RobotHandle();
    public event RobotHandle onPlay;
    public event RobotHandle onRobot;

    [Header("데이터")]
    public SpeedData data;

    private float moveSpeed;       // 이동 속도

    private bool shouldRotateRight = true;  // 오른쪽으로 회전 허용 여부
    private bool shouldRotateLeft = false;  // 왼쪽으로 회전 허용 여부
    private bool hasCollided = false;       // 충돌 여부

    private float angle = 180;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        moveSpeed = data.speed;
    }


    private void Start()
    {
        onRobot?.Invoke();
        Invoke("PlayStart", 5f);
    }

    void Update()
    {
        if (hasCollided)
        {
           if (shouldRotateLeft)
            {
                // 왼쪽으로 1도씩 천천히 회전
                RotateLeft();
            }
            else
            {
                // 이동
                MoveForward();
            }
        }
        else
        {
            if (shouldRotateRight)
            {
                RotateRight();
            }
            else
            {
                MoveForward();
            }
        }
    }

    void RotateRight()
    {
        angle++;

        // 오른쪽으로 90도 회전이 완료되면 왼쪽으로 회전 허용
        if (angle % 90 == 0)
        {
            shouldRotateRight = false;
        }

        transform.parent.rotation = Quaternion.Euler(0, angle, 0);
    }

    void RotateLeft()
    {
        angle--;

        // 오른쪽으로 90도 회전이 완료되면 왼쪽으로 회전 허용
        if (angle % 90 == 0)
        {
            shouldRotateLeft = false;
        }

        transform.parent.rotation = Quaternion.Euler(0, angle, 0);
    }

    void MoveForward()
    {
        // forward 방향으로 이동
        transform.parent.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    // 충돌 감지
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Door"))
        {
            // 충돌 시 왼쪽으로 90도 회전 허용
            shouldRotateLeft = true;
            hasCollided = true;
        }
     
    }

    private void PlayStart()
    {
        onPlay?.Invoke();
        GetComponent<RobotCleanerMovement>().enabled = true;
        GetComponent<RobotStart>().enabled = false;
    }
}
