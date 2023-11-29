using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectHPbar : MonoBehaviour
{
    private float curHP;
    private float maxHP;

    public Slider HPbar;

    public void SetHP(float amount)
    {
        maxHP = amount;
        curHP = maxHP;
    }

    public void ChechHP()
    {
        if (HPbar != null)
        {
            HPbar.value = curHP / maxHP;
        }
    }

    public void Demage(float demage)
    {
        if (maxHP == 0 || curHP <= 0) return;

        curHP -= demage;

        ChechHP();
    }

    public float GetHP()
    {
        return curHP;
    }

    private void Update()
    {
        HPbar.transform.parent.rotation = Quaternion.Euler(0, 90, 0);
    }
}
