using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoapRifle : Weapon
{
    [Header("발사 위치")]
    public Transform shootPosition;

    [Header("프리팹")]
    public GameObject projectilePrefab; // 발사체 프리팹
    public GameObject chargePrefab;     // 차지 프리팹

    [Header("목표뮬")]
    private Vector3 shootTarget;

    public float bulletSpeed = 20f;
    public float distance = 10f;

    public int maxChargeLvel = 3;
    public int curChargeLevel = 0;

    public override void EnterShoot()
    {
        curChargeLevel = 0;
        chargePrefab.transform.localScale = new Vector3(0.2f,0.2f,0.2f);
    }

    private float chargeTime = 0;
    public override void ExcuteShoot()
    {
        chargePrefab.SetActive(true);
        chargeTime += Time.deltaTime;
        if (chargeTime >= 1)
        {
            chargeTime = 0;
            if (curChargeLevel < maxChargeLvel && curChargeLevel + 1 <= curBullet) curChargeLevel++;
            chargePrefab.transform.localScale = curChargeLevel * new Vector3(0.2f,0.2f,0.2f);
        }
    }
    public override void ExitShoot()
    {
        chargePrefab.SetActive(false);
        Shoot();

        useBullet = 1;
    }

    public override void SetTarget(Vector3 direction)
    {
        shootTarget = direction;
    }

    public override void Shoot()
    {
        useBullet *= curChargeLevel + 1;
        if (curBullet < useBullet) return;
        UseBullet();

        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        bullet.transform.position = shootPosition.position;

        SoapProjectile projectile = bullet.GetComponent<SoapProjectile>();
        projectile.ShootBeamInDir(shootPosition.position, shootTarget, curChargeLevel);
        projectile.GetComponent<IProjectile>().SetDamage(damage * damageOffset);
    }
}
