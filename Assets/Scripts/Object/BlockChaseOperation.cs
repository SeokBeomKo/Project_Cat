using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChaseOperation : MonoBehaviour
{

    //public Rigidbody playerRigidbody;
    private bool isFrozen = false;

    public float freezeDuration = 1.0f; // 1초 동안 멈추도록 설정

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 충돌한 오브젝트가 플레이어인지 확인
        {
            Debug.Log("Player Collision");
            Debug.Log("Player HP -1");
            if (!isFrozen)
            {
                StartCoroutine(FreezePlayer(collision));
            }
        }
    }

    private IEnumerator FreezePlayer(Collision collision)
    {
        isFrozen = true;
        Debug.Log("Player Stop");

        collision.rigidbody.velocity = Vector3.zero; // 현재 속도를 0으로 설정하여 멈춥니다.

        yield return new WaitForSeconds(freezeDuration);

        isFrozen = false;
        Debug.Log("Player Move");

    }

}
