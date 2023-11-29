using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOperation : MonoBehaviour
{
    public GameObject Fog;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Fog.gameObject.SetActive(true);
        }
    }
}
