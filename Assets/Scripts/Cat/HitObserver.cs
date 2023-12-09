using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObserver : MonoBehaviour, IObserver, IAttackable
{
    [Header("데이터")]
    public BattleCatDamageData data;

    private float damage = 5f;
    private bool safeCheck = false;

    private void Awake()
    {
        damage = data.damage;
    }

    public void Notify(ISubject subject)
    {
        var safeSubject = subject as SafeSubject;
        if (safeSubject != null)
        {
            safeCheck = safeSubject.safeCheck;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && safeCheck == false)
        {
            Debug.Log("파동 공격");
            GetDamage();
        }
    }

    public float GetDamage()
    {
        return damage;
    }
}
