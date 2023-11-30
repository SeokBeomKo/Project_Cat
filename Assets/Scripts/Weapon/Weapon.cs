using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public int maxBullet;
    public int curBullet;
    public abstract void SetTarget(Vector3 direction);
    public abstract void Shoot();
    public abstract void EnterShoot();
    public abstract void ExcuteShoot();
    public abstract void ExitShoot();

}
