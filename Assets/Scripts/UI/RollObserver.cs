using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollObserver : Observer
{
    [SerializeField]
    private RollSubject rollSubject;

    public Image[] gaugeImageArray;

    public override void Notify(ISubject subject)
    {
        UpdateImage();
    }

    private void UpdateImage()
    {
        for (int i = 0; i < gaugeImageArray.Length; i++)
        {
            if (i < rollSubject.currentGauge)
                gaugeImageArray[i].enabled = true;
            else
                gaugeImageArray[i].enabled = false;
        }
    }
}
