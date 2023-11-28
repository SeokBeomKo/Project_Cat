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
    public int bulletCount = 8; // 한 번에 발사할 총알의 수
    public float bulletSpeed = 10f; // 총알의 속도
    public float spreadAngle;       // 총알이 퍼지는 각도

    [Header("목표뮬")]
    private Vector3 shootTarget;

    public override void EnterShoot()
    {
        FireFlash();
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 spread = Random.insideUnitCircle * spreadAngle;
            // 총구 위치에서 타겟 포인트를 향하는 방향을 계산합니다.
            Vector3 fireDirection = Quaternion.Euler(spread) * (shootTarget - shootPosition.position).normalized;

            // 총알을 발사합니다.
            GameObject bullet = Instantiate(projectilePrefab, shootPosition.position, Quaternion.LookRotation(fireDirection));
            bullet.transform.LookAt(shootTarget);
            // bullet.GetComponent<Rigidbody>().velocity = fireDirection * bulletSpeed;
        }
    }
    public override void ExcuteShoot()
    {
        
    }
    public override void ExitShoot()
    {
        
    }

    public override void SetTarget(Vector3 direction)
    {
        shootTarget = direction;
    }

    public override void Shoot()
    {
        
    }
    public void FireFlash()
    {
        Instantiate(splashPrefab, shootPosition.position,Quaternion.LookRotation(shootTarget));
    }

    public void Fire(Vector3 direction)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 spread = Random.insideUnitCircle * spreadAngle;
            // 총구 위치에서 타겟 포인트를 향하는 방향을 계산합니다.
            Vector3 fireDirection = Quaternion.Euler(spread) * (direction - shootPosition.position).normalized;

            // 총알을 발사합니다.
            // GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.LookRotation(fireDirection));
            // bullet.GetComponent<Rigidbody>().velocity = fireDirection * bulletSpeed;
        }
    }

}
