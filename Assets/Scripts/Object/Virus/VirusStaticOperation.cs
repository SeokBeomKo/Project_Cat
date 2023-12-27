using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStaticOperation : MonoBehaviour, IDamageable
{
    [Header("데이터")]
    public VirusStaticData data;

    // TODO : 옵저버 패턴 .
    private float HP;
    private float damage;

    public ObjectHPbar objectHPbar;

    [Header("모델링 정보")]
    public GameObject model;
    public GameObject explosionVFX;
    public GameObject hpBar;

    public Collider sphereCollider;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        HP = data.hp;
        damage = data.damage;
    }

    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.CheckHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            //Debug.Log(other.gameObject.GetComponentInChildren<IAttackable>().GetDamage());
            Hit(other.gameObject.GetComponentInChildren<IAttackable>().GetDamage());
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
            if (null != collision.transform.GetComponentInChildren<PlayerHitScan>())
            {
                collision.transform.GetComponentInChildren<PlayerHitScan>().GetDamage(damage);
            }
        }
    }

    private void Hit(float damage)
    {
        objectHPbar.Damage(damage);
        HP = objectHPbar.GetHP();
        Check();
    }

    private void Check()
    {
        if (HP <= 0)
        {
            SoundManager.Instance.PlaySFX("VirusDeath");
            model.SetActive(false);
            hpBar.SetActive(false);
            sphereCollider.enabled = false;
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

    public void BeAttacked(float damage)
    {
        Hit(damage);
    }
}
