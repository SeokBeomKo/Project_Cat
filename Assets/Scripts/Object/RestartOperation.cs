using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartOperation : MonoBehaviour
{

    public Transform SpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.transform.parent.position = SpawnPoint.position;
        }
    }
  
}
