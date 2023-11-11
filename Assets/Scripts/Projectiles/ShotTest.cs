using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTest : MonoBehaviour
{
    public Transform shootPosition;
    public GameObject bulletPrefab; // 총알 프리팹
    public int bulletCount = 8; // 한 번에 발사할 총알의 수
    public float bulletSpeed = 10f; // 총알의 속도
    public float spreadAngle = 45f; // 총알이 퍼지는 각도

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // 총구가 바라보는 방향에서 무작위 각도를 계산합니다.
            Vector3 spread = Random.insideUnitCircle * spreadAngle;
            Vector3 direction = Quaternion.Euler(spread) * transform.forward;

            // 총알을 발사합니다.
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(direction));
            bullet.transform.position = shootPosition.position;
            bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        }
    }
}
