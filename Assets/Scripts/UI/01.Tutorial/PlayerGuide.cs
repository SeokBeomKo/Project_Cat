using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuide : MonoBehaviour
{
    public delegate void PlayerGuideHandle();
    public event PlayerGuideHandle OnPlayerGuide;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnPlayerGuide?.Invoke();
            gameObject.SetActive(false);
        }
    }
}