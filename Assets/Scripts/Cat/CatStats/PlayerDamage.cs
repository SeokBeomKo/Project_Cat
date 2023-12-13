using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("데이터")]
    public BattleCatDamageData data;

    private float damage;

    private void Awake()
    {
        data.LoadDataFromPrefs();
        
        damage = data.damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (null != other.transform.GetComponentInChildren<PlayerHitScan>())
            {
                other.transform.GetComponentInChildren<PlayerHitScan>().GetDamage(damage);
            }
        }
    }
}
