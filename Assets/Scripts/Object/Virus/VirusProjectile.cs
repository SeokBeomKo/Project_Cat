using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class VirusProjectile : MonoBehaviour, IAttackable
{
    [Header("프리팹")]
    public GameObject projectile;
    public GameObject explosion;

    [Header("리지드바디")]
    public Rigidbody rigidBody;

    [Header("방향 정보")]
    public Vector3 directionPosition;
    private Vector3 targetDirection;

    [Header("수치 값")]
    public float maxSpeed;

    private void OnEnable()
    {
        Invoke("Explosion", 1f);
        // 5초가 지나면 총아 ㄹ터짐
    }

    public void SetDirection(Vector3 direction)
    {
        // 총알이 가야 할 방향
        directionPosition = direction;
    }

    private void FixedUpdate()
    {
        // 실제 이동
        Debug.Log("Attack : " + directionPosition);
        rigidBody.velocity = directionPosition * maxSpeed;
    }

    // 터지는 함수
    public void Explosion()
    {
        rigidBody.isKinematic = true;

        projectile.SetActive(false);
        explosion.SetActive(true);

        StartCoroutine(DestroyAfterParticles());
    }

    // 터지는 이펙트 끝났을 때 삭
    private IEnumerator DestroyAfterParticles()
    {
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();

        while (ps != null && ps.IsAlive())
        {
            yield return null;
        }

        Destroy(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Explosion();
    }

    private void OnCollisionEnter(Collision other)
    {
        Explosion();
    }

    public float GetDamage()
    {
        return 5;
    }
}
