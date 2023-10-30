using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleNavigateOperation : MonoBehaviour
{
    [Header("아이템 리스트")]
    public List<ItemWithProbability> itemsToSpawn;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            foreach (var item in itemsToSpawn)
            {
                if (Random.value < item.probability)
                {
                    // 아이템을 생성할 위치
                    Vector3 spawnPosition = transform.position;

                    // 아이템 생성
                    Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
                    Debug.Log(item.itemPrefab.tag);
                }
            }

            // 캡슐 오브젝트 비활성화 또는 삭제 (선택적)
            gameObject.SetActive(false);
        }
    }
}
