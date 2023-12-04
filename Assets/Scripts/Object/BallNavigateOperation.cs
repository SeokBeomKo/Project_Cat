using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigateOperation : MonoBehaviour, IDamageable
{
    // TODO : 옵저버 패턴 .
    public float HP = 30;
    public ObjectHPbar objectHPbar;
    public GameObject hpBar;
    public GameObject effect;
    private bool isChange = false;

    // Start is called before the first frame update
    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.CheckHP();
    }

    private void Update()
    {
        Debug.Log("Update");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision " + collision.gameObject.layer);

        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack") && !isChange)
        {

            Debug.Log(collision.gameObject.GetComponentInChildren<IAttackable>().GetDamage());
            Hit(collision.gameObject.GetComponentInChildren<IAttackable>().GetDamage());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.gameObject.layer);

        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack") && !isChange)
        {

          Debug.Log(other.gameObject.GetComponentInChildren<IAttackable>().GetDamage());
            Hit(other.gameObject.GetComponentInChildren<IAttackable>().GetDamage());
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
            hpBar.SetActive(false);
            effect.SetActive(true);
            transform.gameObject.tag = "Ball";
            transform.parent.gameObject.tag = "Ball";
            isChange = true;
        }
    }

    public void BeAttacked(float damage)
    {
        Hit(damage);
    }
}
