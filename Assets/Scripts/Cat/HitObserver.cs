using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObserver : MonoBehaviour, IObserver
{
    PlayerStats playerStats;

    private bool safeCheck = false;

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
            if (playerStats != null)
            {
                playerStats.GetDamage(10);
            }
        }
    }
}
