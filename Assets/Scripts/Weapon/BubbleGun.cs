using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGun : Weapon
{
    public Transform shootPosition;
    public GameObject projectilePrefab; // 발사체 프리팹
    public float fireRate = 1f; // 발사 속도
    public float projectileForce;           // 발사체에 가해질 힘
    public float projectileHoverTime;       // 발사체가 부유하는 시간
    public float trackingDistance = 5f; // 발사체가 추적 시작하는 거리

    private float lastFireTime;

    public override void EnterShot()
    {
        
    }
    public override void ExcuteShot()
    {
        
    }
    public override void ExitShot()
    {
        
    }

    public override void SetDirection(Vector3 direction)
    {
        // 비눗방울을 발사합니다.
        GameObject bullet = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);
        bullet.GetComponent<BubbleProjectile>().SetDirection(direction);
    }
}
