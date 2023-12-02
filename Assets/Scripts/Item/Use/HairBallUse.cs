using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class HairBallUse : MonoBehaviour
{
    public GameObject Player;
    public GameObject HairBall;
    public float obstacleCheckDistance = 1; // 장애물을 확인할 거리

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Debug.Log("Input 9");

            if (!CheckObstacleInFront())
            {
                CreateHairBall();
            }
            else
            {
                Debug.Log("장애물이 있어 생성할 수 없음");
            }
        }
    }

    void CreateHairBall()
    {
        Debug.Log("CreateHairBall");

        // 플레이어의 위치를 얻습니다.
        Vector3 playerPosition = Player.transform.position;

        // 플레이어 앞쪽에 위치를 계산합니다.
        Vector3 spawnPosition = playerPosition + Player.transform.forward * 0.7f;
        spawnPosition.y += 0.1f;

        // 계산된 위치에 HairBall을 생성합니다.
        Instantiate(HairBall, spawnPosition, Quaternion.identity);
    }

    bool CheckObstacleInFront()
    {
        // 플레이어 앞쪽 방향으로 레이캐스트를 수행하여 장애물을 확인합니다.
        Vector3 playerPosition = Player.transform.position;
        Vector3 playerForward = Player.transform.forward;
        RaycastHit hit;

        if (Physics.Raycast(playerPosition, playerForward, out hit, obstacleCheckDistance))
        {
            // 장애물이 감지되면 메시지를 기록하고 true를 반환합니다.
            Debug.Log("장애물 감지: " + hit.collider.gameObject.name);
            return true;
        }

        // 장애물이 감지되지 않으면 false를 반환합니다.
        return false;
    }

}
