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
        SelectSoapRifle();
    }

    public void SelectSoapRifle()
    {
        softRifleCrossHair.SetActive(true);
        splashBusterCrossHair.SetActive(false);
        bubbleGunCrossHair.SetActive(false);
    }

    public void SelectSplashBuster()
    {
        softRifleCrossHair.SetActive(false);
        splashBusterCrossHair.SetActive(true);
        bubbleGunCrossHair.SetActive(false);
    }

    public void SelectBubbleGun()
    {
        softRifleCrossHair.gameObject.SetActive(false);
        splashBusterCrossHair.gameObject.SetActive(false);
        bubbleGunCrossHair.gameObject.SetActive(true);
    }
}
