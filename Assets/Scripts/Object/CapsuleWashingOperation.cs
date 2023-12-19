using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleWashingOperation : MonoBehaviour
{
    public delegate void ItemCreateHandle(Item item);
    public event ItemCreateHandle onItemCreate;

    [Header("아이템 리스트")]
    public List<ItemWithProbability> itemsToSpawn;

    [Header("데이터")]
    public HPData data;
    private float HP;

    public ObjectHPbar objectHPbar;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        HP = data.hp;
    }

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

                        Item tempitem = Instantiate(item.itemPrefab, spawnPosition, Quaternion.identity).GetComponent<Item>();
                        onItemCreate?.Invoke(tempitem);
                        Debug.Log(item.itemPrefab.name);

                        break;
                    }
                }


                // 캡슐 오브젝트 비활성화 또는 삭제 (선택적)
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
