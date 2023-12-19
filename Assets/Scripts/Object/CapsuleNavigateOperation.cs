using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleNavigateOperation : MonoBehaviour
{
    [Header("아이템 리스트")]
    public List<ItemWithProbability> itemsToSpawn;

    public delegate void ItemCreateHandle(Item item);
    public event ItemCreateHandle onItemCreate;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger : " + other.gameObject.layer);

        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            float randomValue = Random.value;
            float cumulativeProbability = 0.0f;

            foreach (var item in itemsToSpawn)
            {
                cumulativeProbability += item.probability;

                if (randomValue < cumulativeProbability)
                {
                    // 아이템을 생성할 위치
                    Vector3 spawnPosition = transform.parent.position;

                    // 아이템 생성
                    Item tempitem = Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity).GetComponent<Item>();
                    onItemCreate?.Invoke(tempitem);

                    // 생성된 아이템이 있으므로 루프 종료
                    break;
                }
            }

            // 캡슐 오브젝트 삭제
            transform.parent.gameObject.SetActive(false);
        }
    }

}
