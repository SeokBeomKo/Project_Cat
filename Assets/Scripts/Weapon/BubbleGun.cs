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

    public override void Fire(Vector3 direction)
    {
        // 비눗방울을 발사합니다.
        GameObject bullet = Instantiate(projectilePrefab, shootPosition.position, Quaternion.identity);
        bullet.GetComponent<BubbleProjectile>().SetDirection((direction - shootPosition.position).normalized);
        // Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // 비눗방울이 위로 떠오르면서 앞으로 나아가는 움직임을 추가합니다.
        // bulletRigidbody.AddForce(direction + Vector3.up * (projectileForce * 0.5f), ForceMode.Force); // 초기 발사 속도를 줄입니다.

        // 발사체에 HoverAndTrack 스크립트를 추가하고 설정합니다.
        // HoverAndTrack hoverAndTrack = bullet.AddComponent<HoverAndTrack>();
        // hoverAndTrack.hoverTime = projectileHoverTime;
        // hoverAndTrack.trackingDistance = trackingDistance;
    }
}
