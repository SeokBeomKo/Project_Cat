using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour, IAttackable
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
            GetDamage();
        }
    }

    public float GetDamage()
    {
        return damage;
    }

}
