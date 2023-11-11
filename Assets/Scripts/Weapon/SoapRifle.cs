using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapRifle : Weapon
{
    public Transform shootPosition;
    public GameObject projectilePrefab; // '가짜' 총알 프리팹
    public float bulletSpeed = 20f;

    public override void Fire()
    {
        // 레이캐스팅을 사용하여 총알이 맞는 위치를 찾습니다.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // '가짜' 총알을 발사합니다.
            GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            bullet.transform.position = shootPosition.position;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;

            // '가짜' 총알을 목표물에 맞는 위치까지 이동시킵니다.
            StartCoroutine(MoveBullet(bullet, hit.point));
        }
    }

    private IEnumerator MoveBullet(GameObject bullet, Vector3 target)
    {
        while ((bullet.transform.position - target).magnitude > 0.1f)
        {
            yield return null;
        }

        Destroy(bullet); // 목표물에 도달하면 '가짜' 총알을 제거합니다.
    }
}
