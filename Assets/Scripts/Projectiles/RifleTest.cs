using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleTest : MonoBehaviour
{
    public Transform shootPosition;
    public GameObject bulletPrefab; // '가짜' 총알 프리팹
    public float bulletSpeed = 20f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Debug.Log("Fire");
        // 레이캐스팅을 사용하여 총알이 맞는 위치를 찾습니다.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            Debug.Log("Hit " + hit.transform.name);

            // '가짜' 총알을 발사합니다.
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            bullet.transform.position = shootPosition.position;

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
