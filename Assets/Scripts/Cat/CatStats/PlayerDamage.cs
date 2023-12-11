using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("ตฅภฬลอ")]
    public BattleCatDamageData data;

    private float damage;

    private void Awake()
    {
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
