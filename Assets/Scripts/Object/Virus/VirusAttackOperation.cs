using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAttackOperation : MonoBehaviour, IAttackable, IDamageable
{
    [Header("데이터")]
    public VirusAttackData data;

    public ObjectHPbar objectHPbar;

    private float HP;
    private float radius;
    private float time;
    private float damage;

    private Vector3 playerPosition;

    [Header("유한 상태 기계")]
    [SerializeField] public VirusStateMachine virusMachine;

    [Header("모델링 정보")]
    public GameObject model;
    public GameObject explosionVFX;

    public GameObject ProjectilePrefab;

    public delegate void VirusRespawnHandle();
    public event VirusRespawnHandle OnRespawnTimerStart;


    private void Awake()
    {
        data.LoadDataFromPrefs();

        HP = data.hp;
        radius = data.range;
        time = data.tiime;
        damage = data.damage;

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    void Update()
    {
        if (null != virusMachine.curState)
        {
            virusMachine.curState.Execute();
        }

    }

    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.CheckHP();
    }

    private void OnEnable()
    {
        model.SetActive(true);       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
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
            collision.transform.GetComponentInChildren<PlayerHitScan>().GetDamage(GetDamage());
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
            objectHPbar.gameObject.GetComponentInParent<GameObject>().SetActive(false);
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

        explosionVFX.SetActive(false);
        OnRespawnTimerStart?.Invoke();
        transform.gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    public void BeAttacked(float damage)
    {
        Hit(damage);
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetRespawnTime()
    {
        return time;
    }

    public float GetRange()
    {
        return radius;
    }

    public Vector3 GetPlayPosition()
    {

        return playerPosition;
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        playerPosition = pos;
    }


}
