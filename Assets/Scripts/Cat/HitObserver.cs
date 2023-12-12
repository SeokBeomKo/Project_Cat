using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObserver : MonoBehaviour, IObserver
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
            safeCheck = safeSubject.currentSafeCheck;
            Debug.Log("HitObserver: safeCheck updated to " + safeCheck);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !safeCheck)
        {
            if (null != other.transform.GetComponentInChildren<PlayerHitScan>())
            {
                other.transform.GetComponentInChildren<PlayerHitScan>().GetDamage(damage);
                Debug.Log("공격!!!! safeCheck - " + safeCheck);
            }
        }
    }
}