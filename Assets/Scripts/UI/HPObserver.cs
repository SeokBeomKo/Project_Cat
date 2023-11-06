using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPObserver : Observer
{
    [SerializeField]
    private HPSubject hpSubject;

    public Image[] hpImageArray;

    public override void Notify(ISubject subject)
    {
        for (int i = 0; i < hpImageArray.Length; i++)
        {
            if (i < hpSubject.hp)
                hpImageArray[i].enabled = true;
            else
                hpImageArray[i].enabled = false;
        }
    }
}
