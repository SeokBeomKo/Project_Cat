using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashProjectile : MonoBehaviour
{
    [Header("프리팹")]
    public GameObject projectile;
    public GameObject explosion;
    
    [Header("리지드바디")]
    public Rigidbody rigidBody;

    private void OnEnable() 
    {
        Invoke("Explosion", 5f);
    }

    public void Explosion()
    {
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
