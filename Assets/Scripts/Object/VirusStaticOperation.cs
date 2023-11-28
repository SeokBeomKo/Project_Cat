using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStaticOperation : MonoBehaviour
{
    // TODO : 옵저버 패턴 .
    public float HP = 5;
    public ObjectHPbar objectHPbar;

    [Header("모델링 정보")]
    public GameObject model;
    public GameObject explosionVFX;

    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.ChechHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            objectHPbar.Demage(1);
            HP = objectHPbar.GetHP();
            Check();
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HP = 0;
            Check();
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player HP--");
        }
    }

    private void Check()
    {
        if (HP == 0)
        {
            model.SetActive(false);
            explosionVFX.SetActive(true);

            StartCoroutine(DestroyAfterParticles());
        }
    }

    private IEnumerator DestroyAfterParticles()
    {
        ParticleSystem ps = explosionVFX.GetComponent<ParticleSystem>();
    
        while (ps != null && ps.IsAlive())
        {
            yield return null;
        }
    
        Destroy(transform.parent.gameObject);
    }
}
