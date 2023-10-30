using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPObserver : Observer
{
    [SerializeField]
    private HPSubject hpSubject;

    public Image[] hpImage;

    public override void Notify(ISubject subject)
    {
        for (int i = 0; i < hpImage.Length; i++)
        {
            if (i < hpSubject.hp)
                hpImage[i].enabled = true;
            else
                hpImage[i].enabled = false;
        }
    }
}
