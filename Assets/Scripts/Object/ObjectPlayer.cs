using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlayer : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도를 조절할 변수
    public float rotationSpeed = 90.0f; // 회전 속도를 조절할 변수

    private void Update()
    {
        // 키 입력을 감지하여 이동 및 회전
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(0, 0, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        float rotation = horizontalInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotation);
    }
}
