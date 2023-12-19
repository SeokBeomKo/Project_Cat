using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCenter : MonoBehaviour
{
    [SerializeField]
    private VirusAttackOperation[] virus;
    [SerializeField] public GameObject Object;

    [SerializeField]
    private float respawnTime;

    [Header("아이템 리스트")]
    public List<ItemWithProbability> itemsToSpawn;

    private float itemAppearTime = 10f;
    private float itemDisappearTime = 15f;
    private float minYPosition = -1.614f;
    private float maxYPosition = 1.385f;

    public delegate void ItemCreateHandle(Item item);
    public event ItemCreateHandle onItemCreate;

    //private int index;

    void Start()
    {
        if (itemsToSpawn.Count > 0)
        {
            foreach (var item in itemsToSpawn)
            {
                item.probability = 1.0f / itemsToSpawn.Count;
            }

            virus = Object.GetComponentsInChildren<VirusAttackOperation>();
        }

        for (int i = 0; i < virus.Length; i++)
        {
            int index = i;

            virus[index].OnRespawnTimerStart += () => OnRespawnTimer(index);

        }

        PlayerPrefs.SetString("nextScene", "08.Battle");

        // Item Create
        StartCoroutine(SpawnItemsPeriodically());
    }

    public void OnRespawnTimer(int index)
    {
        StartCoroutine(Respawn(index));
    }

    protected IEnumerator Respawn(int index)
    {
        yield return new WaitForSeconds(respawnTime);

        virus[index].transform.parent.gameObject.SetActive(true);
        virus[index].transform.gameObject.SetActive(true);
    }

    protected IEnumerator SpawnItemsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(itemAppearTime); // 10초 대기

            SpawnRandomItem();
        }
    }

    protected void SpawnRandomItem()
    {
        if (itemsToSpawn.Count > 0)
        {
            float randomValue = Random.value;
            float cumulativeProbability = 0.0f;

            foreach (var item in itemsToSpawn)
            {
                cumulativeProbability += item.probability;

                if (randomValue < cumulativeProbability)
                {
                    // 아이템을 생성할 위치
                    Vector3 randomSpawnPosition = new Vector3(Random.Range(35.05f, 43.51f), maxYPosition, Random.Range(-5.43f, 3.05f)); // 예시로 x, z축은 -5에서 5 사이의 랜덤한 값으로 설정

                    GameObject newItem = Instantiate(item.itemPrefab, randomSpawnPosition, Quaternion.identity);

                    onItemCreate?.Invoke(newItem.GetComponent<Item>());

                    StartCoroutine(MoveItemDown(newItem.transform));

                    StartCoroutine(DestroyItemAfterTime(newItem));

                    // 생성된 아이템이 있으므로 루프 종료
                    break;
                }
            }
        }

    }

    protected IEnumerator MoveItemDown(Transform itemTransform)
    {
        float moveSpeed = 2f; // 아이템의 떨어지는 속도

        while (itemTransform.position.y > minYPosition)
        {
            itemTransform.position += Vector3.down * moveSpeed * Time.deltaTime;

            yield return null;
        }

    }

    protected IEnumerator DestroyItemAfterTime(GameObject item)
    {
        yield return new WaitForSeconds(itemDisappearTime);
        Destroy(item);
    }


}
