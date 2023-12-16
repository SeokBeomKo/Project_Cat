using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputCenter : MonoBehaviour
{
    [Header("UI 인풋 핸들")]
    public UIInputHandler uiHandle;

    [Header("세팅창")]
    public Setting setting;

    [Header("세척도 팝업")]
    public CleanlinessPopUpObserver clean;

    [Header("크로스 헤어")]
    public CrossHairSelection crossHair;

    private void Start()
    {
        uiHandle.OnPausePopUp += ActivePausePopUp;
        if (clean != null)
        {
            uiHandle.OnCleanlinessPopUpTrue += ActiveCleanPopUp;
            uiHandle.OnCleanlinessPopUpFalse += DeactiveCleanPopUp;
        }
        uiHandle.OnSelectSoapRifle += OnSoapRifle;
        uiHandle.OnSelectSplashBuster += OnSplashBuster;
        uiHandle.OnselectBubbleGun += OnBubbleGun;
    }

    public void ActivePausePopUp()
    {
        setting.UpdatePause();
    }

    public void ActiveCleanPopUp()
    {
        clean.ActivateCleanliness();
    }

    public void DeactiveCleanPopUp()
    {
        clean.DeactivateCleanliness();
    }

    public void OnSoapRifle()
    {
        crossHair.SelectSoapRifle();
    }

    public void OnSplashBuster()
    {
        crossHair.SelectSplashBuster();
    }

    public void OnBubbleGun()
    {
        crossHair.SelectBubbleGun();
    }
}
