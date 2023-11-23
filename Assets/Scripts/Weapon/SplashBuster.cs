using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SplashBuster : Weapon
{
    public Transform shootPosition;
    public GameObject bulletPrefab; // 총알 프리팹
    public int bulletCount = 8; // 한 번에 발사할 총알의 수
    public float bulletSpeed = 10f; // 총알의 속도
    public float spreadAngle;       // 총알이 퍼지는 각도

    public override void EnterShot()
    {
        
    }
    public override void ExcuteShot()
    {
        
    }
    public override void ExitShot()
    {
        
    }

    public override void SetTarget(Vector3 direction)
    {
    }

    public override void Shoot()
    {
        
    }

    public void Fire(Vector3 direction)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 spread = Random.insideUnitCircle * spreadAngle;
            // 총구 위치에서 타겟 포인트를 향하는 방향을 계산합니다.
            Vector3 fireDirection = Quaternion.Euler(spread) * (direction - shootPosition.position).normalized;

            // 총알을 발사합니다.
            GameObject bullet = Instantiate(bulletPrefab, shootPosition.position, Quaternion.LookRotation(fireDirection));
            bullet.GetComponent<Rigidbody>().velocity = fireDirection * bulletSpeed;
        }
    }

}
