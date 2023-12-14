using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCatMove : MonoBehaviour
{
    public delegate void ExitCatHandle();
    public event ExitCatHandle OnExitCat;
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Invoke("ExitCat", 0.5f);
        }
    }

    void ExitCat()
    {
        OnExitCat?.Invoke();
    }
}
