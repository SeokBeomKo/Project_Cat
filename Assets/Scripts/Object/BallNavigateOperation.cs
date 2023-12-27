using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigateOperation : MonoBehaviour, IDamageable, IInteractable
{
    [Header("데이터")]
    public HPData data;

    // TODO : 옵저버 패턴 .
    private float HP;
    public ObjectHPbar objectHPbar;
    public GameObject hpBar;
    public GameObject effect;
    private bool isChange = false;

    [SerializeField]
    public Rigidbody rigidBody;

    private void Awake()
    {
        data.LoadDataFromPrefs();

        HP = data.hp;
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
            Interaction(other.transform.position, other.gameObject.GetComponentInChildren<IAttackable>().GetDamage());
        }
    }

    public void Interaction(Vector3 interPos, float damage = 5f)
    {
        if (!isChange)
        {
            Hit(damage);
        }
        Vector3 direction = (transform.position - interPos).normalized;
        rigidBody.AddForce(direction * 10, ForceMode.Impulse);
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
            hpBar.SetActive(false);
            effect.SetActive(true);
            transform.gameObject.tag = "Ball";
            //transform.parent.gameObject.tag = "Ball";
            isChange = true;
        }
    }

    public void BeAttacked(float damage)
    {
        Hit(damage);
    }

    
}
