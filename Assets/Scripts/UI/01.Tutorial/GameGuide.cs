using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGuide : MonoBehaviour
{
    public GameObject keyGuide;
    public GameObject itemGuide;
    public GameObject gunGuide;

    public void ShowKeyPopUp()
    {
        keyGuide.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseKeyPopUp()
    {
        keyGuide.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowItemPopUp()
    {
        itemGuide.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseItemPopUp()
    {
        itemGuide.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowGunPopUp()
    {
        gunGuide.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseGunPopUp()
    {
        gunGuide.SetActive(false);
        Time.timeScale = 1f;
    }
}
