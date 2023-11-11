using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollObserver : MonoBehaviour, IObserver
{
    public Image[] gaugeImageArray;

    public void Notify(ISubject subject)
    {
        UpdateImage(subject as PlayerStats);
    }

    private void UpdateImage(PlayerStats playerStats)
    {
        for (int i = 0; i < gaugeImageArray.Length; i++)
        {
            if (i < playerStats.currentRoll)
                gaugeImageArray[i].enabled = true;
            else
                gaugeImageArray[i].enabled = false;
        }
    }
}
