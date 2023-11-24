using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStaticOperation : MonoBehaviour
{
    public int HP = 5;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack"))
        {
            HP--; 
        }

        if(collision.gameObject.CompareTag("Ball"))
        {
            HP = 0;
        }

        if(collision.gameObject.CompareTag("Player"))
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
