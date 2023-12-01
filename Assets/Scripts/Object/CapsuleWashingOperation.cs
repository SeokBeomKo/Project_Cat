using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleWashingOperation : MonoBehaviour
{
    [Header("아이템 리스트")]
    public List<ItemWithProbability> itemsToSpawn;

    public float HP = 5;

    public ObjectHPbar objectHPbar;

    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.CheckHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            objectHPbar.Damage(1);
            HP = objectHPbar.GetHP();

            Debug.Log("HP : " + HP);

            if (HP == 0)
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
                        Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity);
                        Debug.Log(item.itemPrefab.name);

                        // 생성된 아이템이 있으므로 루프 종료
                        break;
                    }
                }


                // 캡슐 오브젝트 비활성화 또는 삭제 (선택적)
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
