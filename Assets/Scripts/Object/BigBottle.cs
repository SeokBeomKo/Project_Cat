using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBottle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("탄약 풀 충전 ");
            transform.parent.gameObject.SetActive(false);

        }
    }
}
