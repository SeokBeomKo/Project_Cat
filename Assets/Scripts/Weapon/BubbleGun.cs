using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : Weapon
{
    [Header("발사 위치")]
    public Transform shootPosition;

    [Header("발사체 프리팹")]
    public GameObject projectilePrefab; // 발사체 프리팹

    [Header("진행 방향")]
    private Vector3 shootDirection;

    public float fireRate = 1f; // 발사 속도
    public float projectileForce;           // 발사체에 가해질 힘
    public float projectileHoverTime;       // 발사체가 부유하는 시간
    public float trackingDistance = 5f; // 발사체가 추적 시작하는 거리

    [Header("발사 딜레이")]
    public float shootDelay;

    public override void EnterShot()
    {
        
    }
    public override void ExcuteShot()
    {
        StartCoroutine(ShootCoroutine());
    }

    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    public override void ExitShot()
    {
        StopCoroutine(ShootCoroutine());
    }

    public override void SetTarget(Vector3 direction)
    {
        shootDirection = direction;
    }

    public override void Shoot()
    {
        // 비눗방울을 발사합니다.
        GameObject bullet = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);
        bullet.GetComponent<BubbleProjectile>().SetDirection(shootDirection);
    }
}
