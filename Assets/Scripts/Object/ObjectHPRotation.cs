using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHPRotation : MonoBehaviour
{
    public Transform player;  // 플레이어 오브젝트의 Transform 컴포넌트를 연결해주세요.
    public float rotationSpeed = 5f;  // 회전 속도를 조절합니다.

    void Update()
    {
        if (player != null)
        {
            // 플레이어를 정면으로 향한 벡터를 얻음
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.y = 0f;  // Y 축 값을 0으로 설정하여 수직 회전을 방지

            // 플레이어 정면 방향으로만 회전
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            
        }
        else
        {
            Debug.LogError("Player reference is not set. Please assign the player GameObject to the script.");
        }
    }
}
