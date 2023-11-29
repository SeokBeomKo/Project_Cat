using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SplashBuster : Weapon
{
    public Transform shootPosition;
    [Header("프리팹")]
    public GameObject projectilePrefab; // 발사체 프리팹
    public GameObject splashPrefab;     // 발사 이펙트 프리팹
    public int bulletCount; // 한 번에 발사할 총알의 수
    public float bulletSpeed; // 총알의 속도
    public float spreadAngle;       // 총알이 퍼지는 각도

    [Header("목표뮬")]
    private Vector3 shootTarget;

    [Header("발사 딜레이")]
    public float shootDelay;
    private float lastShootTime;

    private bool isShooting = false;


    // : 마우스 클릭 시
    public override void EnterShoot()
    {
        if (Time.time - lastShootTime >= shootDelay) 
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    // : 마우스 클릭 중
    public override void ExcuteShoot()
    {
        if (Time.time - lastShootTime >= shootDelay) 
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    // : 마우스 클릭 뗌
    public override void ExitShoot()
    {
        isShooting = false;
    }

    public override void SetTarget(Vector3 target)
    {
        shootTarget = target;
    }

    public override void Shoot()
    {
        Flash();
        Fire();
    }

    public void Flash()
    {
        Instantiate(splashPrefab, shootPosition.position,Quaternion.LookRotation(shootTarget));
    }

    public void Fire()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 spread = Random.insideUnitSphere * spreadAngle;
            // 총구 위치에서 타겟 포인트를 향하는 방향을 계산합니다.
            Vector3 fireDirection = Quaternion.Euler(spread) * (shootTarget - shootPosition.position).normalized;

            // 총알을 발사합니다.
            GameObject bullet = Instantiate(projectilePrefab, shootPosition.position, Quaternion.LookRotation(fireDirection));
            bullet.transform.LookAt(fireDirection);
            bullet.GetComponentInChildren<SplashProjectile>().SetDirection(fireDirection);
        }
    }

}
