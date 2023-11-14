using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneProgress : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            Debug.Log("Robot Cleaner Move Start");
        }
    }
}
