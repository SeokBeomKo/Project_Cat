using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public delegate void WeaponBulletHandle();
    public event WeaponBulletHandle OnWeaponBullet;

    public int maxBullet;
    private int _curBullet;
    public int curBullet 
    {
        get
        { return _curBullet; }
        set 
        {
            _curBullet = value;
            OnWeaponBullet?.Invoke();
        }
    }
    public int useBullet;
    public int damage;
    public float damageOffset;

    public abstract void SetTarget(Vector3 direction);
    public abstract void Shoot();
    public abstract void EnterShoot();
    public abstract void ExcuteShoot();
    public abstract void ExitShoot();

    private void Awake() 
    {
        ChargeBullet(maxBullet);
    }

    public void SetOffset(float _offset)
    {
        damageOffset = _offset;
    }

    public void ChargeBullet(int count)
    {
        curBullet += count;
        if (curBullet > maxBullet)  curBullet = maxBullet;
    }
    public void UseBullet()
    {
        curBullet -= useBullet;
        if (curBullet < 0)  curBullet = 0;
    }
    public int GetBullet()
    {
        return curBullet;
    }
}
