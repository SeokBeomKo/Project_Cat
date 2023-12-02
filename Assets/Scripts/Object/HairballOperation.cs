using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairballOperation : MonoBehaviour
{
    [Header("HP")]
    public float HP = 5;
    public ObjectHPbar objectHPbar;

    void Start()
    {
        objectHPbar.SetHP(HP);
        objectHPbar.CheckHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 바이러스 공격 
        if (other.gameObject.layer==LayerMask.NameToLayer("EnemyAttack"))
        {
            Hit();
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        // 고양이 공격
        if (collision.gameObject.CompareTag("Cat")) // 고양이 충돌
        {
            HP = 0;
            Check();
        }
    }

    private void Hit()
    {
        objectHPbar.Damage(1);
        HP = objectHPbar.GetHP();
        Check();
    }

    private void Check()
    {
        if (HP == 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

}
