using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNavigateOperation : MonoBehaviour, IDamageable, IInteractable
{
    // TODO : 옵저버 패턴 .
    public float HP = 30;
    public ObjectHPbar objectHPbar;
    public GameObject hpBar;
    public GameObject effect;
    private bool isChange = false;

    [SerializeField]
    public Rigidbody rigidBody;

    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.CheckHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            Interaction(other.transform.position);
        }
    }

    public void Interaction(Vector3 interPos, float damage = 0)
    {
        if (isChange)
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
