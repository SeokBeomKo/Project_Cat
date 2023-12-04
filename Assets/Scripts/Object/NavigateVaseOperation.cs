using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateVaseOperation : MonoBehaviour
{
    GameObject parent;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag =="Player")
        {
            Debug.Log("탄약 30 충전");
            SoundManager.Instance.PlaySFX("GetItem");
            transform.parent.gameObject.SetActive(false);
        }
    }
}
