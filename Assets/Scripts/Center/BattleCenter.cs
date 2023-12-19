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

    //private int index;

    void Start()
    {
        virus = Object.GetComponentsInChildren<VirusAttackOperation>();

        for (int i = 0; i < virus.Length; i++)
        {
            int index = i;

            virus[index].OnRespawnTimerStart += () => OnRespawnTimer(index);

        }

        PlayerPrefs.SetInt("Restart", 3);

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
            yield return new WaitForSeconds(10f); // 10초 대기

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
                    Vector3 randomSpawnPosition = new Vector3(Random.Range(-5f, 5f), 10f, Random.Range(-5f, 5f)); // 예시로 x, z축은 -5에서 5 사이의 랜덤한 값으로 설정

                    // 아이템 생성
                    Instantiate(item.itemPrefab, randomSpawnPosition, Quaternion.identity);
                    Debug.Log(item.itemPrefab.name);

                    // 생성된 아이템이 있으므로 루프 종료
                    break;
                }
            }

        }
    }


}
