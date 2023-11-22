using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoapRifle : Weapon
{
    public Transform shootPosition;
    public GameObject projectilePrefab; // '가짜' 총알 프리팹
    public float bulletSpeed = 20f;
    public float distance = 10f;

    public override void Fire(Vector3 direction)
    {
        // 레이캐스팅을 사용하여 총알이 맞는 위치를 찾습니다.
        RaycastHit hit;
        GameObject bullet = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        bullet.transform.position = shootPosition.position;
        Vector3 fireDirection = (direction - shootPosition.position).normalized;
        if (Physics.Raycast(transform.position, direction * distance , out hit))
        {
            // '가짜' 총알을 발사합니다.
            bullet.GetComponent<Rigidbody>().velocity = fireDirection * bulletSpeed;

            // '가짜' 총알을 목표물에 맞는 위치까지 이동시킵니다.
            StartCoroutine(MoveBullet(bullet, hit.point));
        }
        else
        {
            bullet.GetComponent<Rigidbody>().velocity = fireDirection * bulletSpeed;
            StartCoroutine(MoveBullet(bullet, direction * 10f));
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
