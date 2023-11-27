using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStaticOperation : MonoBehaviour
{
    public float HP = 5;
    public ObjectHPbar objectHPbar;

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

    private void Update()
    {
        if (HP == 0)
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
