using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private IWeapon weapon;

    public void SetWeapon(IWeapon weapon)
    {
        this.weapon = weapon;
    }

    public void Shoot()
    {
        weapon.Shoot();
    }
}
