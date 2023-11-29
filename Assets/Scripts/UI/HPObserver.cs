using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HPObserver : MonoBehaviour, IObserver
{
    public Image[] hpImageArray;
    public TextMeshProUGUI hpText;

    public void Notify(ISubject subject)
    {
        var playerStats = subject as PlayerStats;
        for (int i = 0; i < hpImageArray.Length; i++)
        {
            if (i < playerStats.currentHealth / 5)
                hpImageArray[i].enabled = true;
            else
                hpImageArray[i].enabled = false;
        }

        hpText.text = playerStats.currentHealth.ToString();
    }
}
