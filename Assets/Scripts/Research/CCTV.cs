using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTV : MonoBehaviour
{
    public Material Screen;
    public GameObject interactiveKey;

    private bool hasput = false;
    private bool isPlayerCollision = false;

    public delegate void CCTVHandle();
    public event CCTVHandle OnCCTV;

    void Start()
    {
        Screen.DisableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isPlayerCollision)
        {
            hasput = true;
            OnCCTV?.Invoke();
            interactiveKey.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerCollision = true;
            Screen.EnableKeyword("_EMISSION");

            if (!hasput)
            {
                interactiveKey.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerCollision = false;

        if (!hasput)
        {
            interactiveKey.SetActive(false);
        }
    }
}
