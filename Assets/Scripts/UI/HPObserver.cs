using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HPObserver : Observer
{
    [SerializeField]
    private HPSubject hpSubject;

    public Image[] hpImageArray;
    public TextMeshProUGUI hpText;

    public override void Notify(ISubject subject)
    {

        Debug.Log(hpSubject.hp);
        for (int i = 0; i < hpImageArray.Length; i++)
        {
            if (i < hpSubject.hp / 5)
                hpImageArray[i].enabled = true;
            else
                hpImageArray[i].enabled = false;
        }

        hpText.text = hpSubject.hp.ToString();
    }
}
