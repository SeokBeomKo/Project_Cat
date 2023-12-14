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

    //private int index;

    void Start()
    {
        virus = Object.GetComponentsInChildren<VirusAttackOperation>();

        for (int i = 0; i < virus.Length; i++)
        {
            int index = i;

            virus[index].OnRespawnTimerStart += () => OnRespawnTimer(index);

        }
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

    
}
