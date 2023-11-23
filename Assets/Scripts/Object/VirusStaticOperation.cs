using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusStaticOperation : MonoBehaviour
{
    public int HP = 5;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerAttack") || collision.gameObject.CompareTag("Ball"))
        {
            HP--;

            if(HP==0)
            {
                transform.parent.gameObject.SetActive(false);
            }
        }

        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player HP--");
        }
    }

    private void Update()
    {
        
    }
}
