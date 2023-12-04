using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSubtitle : MonoBehaviour
{
    public delegate void PlayerSubtitleHandle();
    public event PlayerSubtitleHandle OnPlayerSubtitle;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            OnPlayerSubtitle?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
