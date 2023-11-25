using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveKey : MonoBehaviour
{
    public GameObject interactiveImage;

    private void Start()
    {
        interactiveImage.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactiveImage.SetActive(true);
            SoundManager.Instance.PlaySFX("BlockHit");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactiveImage.SetActive(false);
        }
    }
}
