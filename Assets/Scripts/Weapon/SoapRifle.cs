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
    }

    private float chargeTime;
    public override void ExcuteShoot()
    {
        chargePrefab.SetActive(true);
        chargeTime += Time.deltaTime;
        if (chargeTime >= 1)
        {
            chargeTime = 0;
            if (curChargeLevel < maxChargeLvel) curChargeLevel++;
        }
    }
    public override void ExitShoot()
    {
        chargePrefab.SetActive(false);
        Shoot();
    }

    public override void SetTarget(Vector3 direction)
    {
        shootTarget = direction;
    }

    public override void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        bullet.transform.position = shootPosition.position;

        switch(curChargeLevel)
        {
            case 1:
                bullet.transform.localScale *= 1.5f;
                break;
            case 2:
                bullet.transform.localScale *= 2f;
                break;
            case 3:
                bullet.transform.localScale *= 2.5f;
                break;
        }

        SoapProjectile projectile = bullet.GetComponent<SoapProjectile>();
        projectile.ShootBeamInDir(shootPosition.position, shootTarget);
    }
}
