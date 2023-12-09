using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public delegate void WeaponBulletHandle();
    public event WeaponBulletHandle OnWeaponBullet;

    [Header("데이터")]
    public WeaponData data;

    [Header("수치 값")]
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
    public int minDamage;
    public int maxDamage;
    public float damageOffset;

    [Header("수치 값")]
    public float shootDelay;
    protected float lastShootTime;

    public abstract void SetTarget(Vector3 direction);
    public abstract void Shoot();
    public abstract void EnterShoot();
    public abstract void ExcuteShoot();
    public abstract void ExitShoot();

    private void Awake() 
    {
        // 데이터 불러오기
        maxBullet = data.maxBullet;
        minDamage = data.minDamage;
        maxDamage = data.maxDamage;
        shootDelay = data.shootDelay;
    }

    private void Start() 
    {
        ChargeBullet(maxBullet);
    }

    public float GetDamage()
    {
        return Random.Range(minDamage,maxDamage) * damageOffset;
    }

    public void SetOffset(int offset)
    {
        damageOffset = offset;
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
