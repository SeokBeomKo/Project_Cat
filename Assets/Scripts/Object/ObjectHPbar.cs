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

    public void CheckHP()
    {
        if (HPbar != null)
        {
            HPbar.value = curHP / maxHP;
        }
    }

    public void Damage(float damage)
    {
        if (maxHP == 0 || curHP <= 0) return;

        curHP -= damage;

        CheckHP();
    }

    public float GetHP()
    {
        return curHP;
    }

}
