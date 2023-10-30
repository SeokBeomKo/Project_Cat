using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAttack : MonoBehaviour
{
    bool check = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("HitBox") && check == false)
        { 
            Debug.Log("충돌!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeBox"))
        {
            check = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SafeBox"))
        {
            check = true;
            Debug.Log("충돌X");
        }
    }
}
