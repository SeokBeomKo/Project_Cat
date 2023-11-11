using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCenter : MonoBehaviour
{
    public List<Weapon> weaponList;
    private Weapon curWeapon;

    private void Start()
    {
        InitWeapon();
        SwapWeapon(0);
    }

    public void InitWeapon()
    {
        foreach(Weapon obj in weaponList)
        {
            obj.transform.parent.gameObject.SetActive(false);
        }
    }

    public void SwapWeapon(int number)
    {
        if (null != curWeapon)
            curWeapon.transform.parent.gameObject.SetActive(false);

        curWeapon = weaponList[number];
        curWeapon.transform.parent.gameObject.SetActive(true);
    }

    public void FireWeapon()
    {
        curWeapon.Fire();
    }
}
