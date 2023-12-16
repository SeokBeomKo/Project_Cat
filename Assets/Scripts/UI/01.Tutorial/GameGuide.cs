using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGuide : MonoBehaviour
{
    public GameObject keyGuide;
    public GameObject itemGuide;
    public GameObject gunGuide;
    public GameObject virusGuide;

    public delegate void GuideHandler();
    public event GuideHandler OnCloseKey;
    public event GuideHandler OnCloseItem;
    public event GuideHandler OnCloseGun;
    public event GuideHandler OnCloseVirus;

    public void ShowKeyPopUp()
    {
        keyGuide.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseKeyPopUp()
    {
        keyGuide.SetActive(false);
        OnCloseKey?.Invoke();
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
        OnCloseItem?.Invoke();
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
        OnCloseGun?.Invoke();
    }

    public void ShowVirusPopUp()
    {
        virusGuide.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseVirusPopUp()
    {
        virusGuide.SetActive(false);
        Time.timeScale = 1f;
        OnCloseVirus?.Invoke();
    }
}
