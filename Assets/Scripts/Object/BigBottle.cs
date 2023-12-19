using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBottle : MonoBehaviour
{
    public delegate void BigBottleHandle(string name);
    public event BigBottleHandle OnChargeAll;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SoundManager.Instance.PlaySFX("GetItem");
            Debug.Log("item all charge");
            OnChargeAll?.Invoke("BigBottle");
            transform.parent.gameObject.SetActive(false);
        }
    }
}
