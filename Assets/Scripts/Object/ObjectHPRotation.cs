using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHPRotation : MonoBehaviour
{
    public Transform player;  // 플레이어 오브젝트의 Transform 컴포넌트를 연결해주세요.
    public float rotationSpeed = 5f;  // 회전 속도를 조절합니다.

    void Update()
    {
        // 캔버스가 카메라를 바라보게 함
        transform.LookAt(Camera.main.transform.position);

        // 캔버스가 카메라를 정확히 바라보게 하기 위해 회전 값을 반전
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
