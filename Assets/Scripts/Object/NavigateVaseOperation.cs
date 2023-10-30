using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateVaseOperation : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag =="Player")
        {
            Debug.Log("탄약 30 충전");
            gameObject.SetActive(false);
        }
    }
}
