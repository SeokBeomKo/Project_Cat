using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOperation : MonoBehaviour
{
    public GameObject Fog;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Fog.gameObject.SetActive(true);
        }
    }
}
