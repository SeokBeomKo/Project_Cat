using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCenter : MonoBehaviour
{
    Weapon myWeapon;

    private void Start()
    {
        myWeapon = new Weapon();
        myWeapon.SetWeapon(new BubbleGun());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("BubbleGun");
            ChangeBubbleGun();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("SoapRifle");
            ChangeSoapRifle();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("SplashBuster");
            ChangeSplashBuster();
        }

        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
        }
    }


    public void ChangeBubbleGun()
    {
        myWeapon.SetWeapon(new BubbleGun());
    }

    public void ChangeSoapRifle()
    {
        myWeapon.SetWeapon(new SoapRifle());
    }

    public void ChangeSplashBuster()
    {
        myWeapon.SetWeapon(new SplashBuster());
    }

    public void FireWeapon()
    {
        myWeapon.Shoot();
    }
}
