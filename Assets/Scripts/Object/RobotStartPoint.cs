using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStartPoint : MonoBehaviour
{
    public RobotStart robotStart;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            robotStart.enabled = true;
        }
    }
}
