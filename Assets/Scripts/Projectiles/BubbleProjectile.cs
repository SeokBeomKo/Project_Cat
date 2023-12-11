using System.Collections;
using UnityEngine;

public class BubbleProjectile : MonoBehaviour, IAttackable, IProjectile
{
    [Header("프리팹")]
    public GameObject projectile;
    public GameObject explosion;

    [Header("리지드바디")]
    public Rigidbody rigidBody;

    [Header("콜라이더")]
    public Collider projectileCollider;

    [Header("방향 정보")]
    public Vector3 directionPosition;
    private Vector3 targetDirection;

    [Header("수치 값")]
    public float maxSpeed;

    private float damage;

    private float time;


    public void SetDamage(float _damage)
    {
        damage = _damage;
    }

    public float GetDamage()
    {
        return damage;
    }

    private void OnEnable() 
    {
        Invoke("Explosion", 5f);
    }

    public void SetDirection(Vector3 direction)
    {
        directionPosition = direction;
        targetDirection = (directionPosition - transform.position).normalized;
        // rigidBody.AddForce(Vector3.up * 200f, ForceMode.Force);
    }
    
    private void FixedUpdate() 
    {
        UseGravity();
        SpeedContoll();
    }

    public void UseGravity()
    {
        time += Time.deltaTime * 7.5f;

        float yForce = Mathf.Sin(time) * 2f;
        rigidBody.AddForce(new Vector3(0, yForce, 0), ForceMode.Force);

        //rigidBody.AddForce(Vector3.down * Mathf.Sin(Time.deltaTime) * 10f, ForceMode.Force);
    }

    public void SpeedContoll()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z);
        }
        else
        {
            rigidBody.AddForce(targetDirection * maxSpeed, ForceMode.Force);
        }
    }

    public void Explosion()
    {
        if(projectileCollider.enabled)
        {
            SoundManager.Instance.PlaySFX("ExplosionBubbleGun");
        }
        projectileCollider.enabled = false;
        rigidBody.isKinematic = true;

        projectile.SetActive(false);
        explosion.SetActive(true);

        StartCoroutine(DestroyAfterParticles());
    }
    
    private IEnumerator DestroyAfterParticles()
    {
        ParticleSystem ps = explosion.GetComponent<ParticleSystem>();
    
        while (ps != null && ps.IsAlive())
        {
            yield return null;
        }
    
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        Explosion();
    }

    private void OnCollisionEnter(Collision other) 
    {
        Explosion();
    }
}
