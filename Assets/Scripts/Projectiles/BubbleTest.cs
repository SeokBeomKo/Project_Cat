using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTest : MonoBehaviour
{
    public Transform shootPosition;
    public GameObject projectilePrefab; // 발사체 프리팹
    public float fireRate = 1f; // 발사 속도
    public float projectileForce = 300f; // 발사체에 가해질 힘
    public float projectileHoverTime = 1f; // 발사체가 부유하는 시간
    public float trackingDistance = 5f; // 발사체가 추적 시작하는 거리

    private float lastFireTime;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > lastFireTime + 1f / fireRate)
        {
            Fire();
            lastFireTime = Time.time;
        }
    }

    private void Fire()
    {
        // 발사체를 생성하고 발사합니다.
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.transform.position = shootPosition.position;
        projectile.GetComponent<Rigidbody>().AddForce(transform.forward * projectileForce);

        // 발사체에 HoverAndTrack 스크립트를 추가하고 설정합니다.
        HoverAndTrack hoverAndTrack = projectile.AddComponent<HoverAndTrack>();
        hoverAndTrack.hoverTime = projectileHoverTime;
        hoverAndTrack.trackingDistance = trackingDistance;
    }
}
