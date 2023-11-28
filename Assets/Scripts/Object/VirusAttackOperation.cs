using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAttackOperation : MonoBehaviour
{
    public float HP = 5;
    public ObjectHPbar objectHPbar;

    public float radius = 0.1f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, radius);
    }

    void Update()
    {
        Collider[] colliders =
                    Physics.OverlapSphere(this.transform.position, radius);

        foreach (Collider col in colliders)
        {
            if (col.name == "Sphere" /* 자기 자신은 제외 */) continue;

            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("virus attack");
            }
        }
    }


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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            HP = 0;
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
            transform.parent.gameObject.SetActive(false);
        }
    }
}
