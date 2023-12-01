using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CrossHairSelection : MonoBehaviour
{
    public GameObject softRifleCrossHair;
    public GameObject splashBusterCrossHair;
    public GameObject bubbleGunCrossHair;


    void Start()
    {
        softRifleCrossHair.SetActive(true);
        splashBusterCrossHair.SetActive(false);
        bubbleGunCrossHair.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManager.Instance.PlaySFX("Hover");
            softRifleCrossHair.SetActive(true);
            splashBusterCrossHair.SetActive(false);
            bubbleGunCrossHair.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManager.Instance.PlaySFX("Hover");
            softRifleCrossHair.SetActive(false);
            splashBusterCrossHair.SetActive(true);
            bubbleGunCrossHair.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SoundManager.Instance.PlaySFX("Hover");
            softRifleCrossHair.gameObject.SetActive(false);
            splashBusterCrossHair.gameObject.SetActive(false);
            bubbleGunCrossHair.gameObject.SetActive(true);
        }
    }
}
