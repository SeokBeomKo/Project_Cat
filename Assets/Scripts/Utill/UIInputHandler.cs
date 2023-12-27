using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    public delegate void UIInputHandle();
    public event UIInputHandle OnCleanlinessPopUpTrue;
    public event UIInputHandle OnCleanlinessPopUpFalse;
    public event UIInputHandle OnPausePopUp;
    public event UIInputHandle OnSelectSoapRifle;
    public event UIInputHandle OnSelectSplashBuster;
    public event UIInputHandle OnselectBubbleGun;

    void Update()
    {
        // ¼¼Ã´µµ ÆË¾÷
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnCleanlinessPopUpTrue?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            OnCleanlinessPopUpFalse?.Invoke();
        }

        // ÀÏ½ÃÁ¤Áö ÆË¾÷
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPausePopUp?.Invoke();
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnSelectSoapRifle?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnSelectSplashBuster?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnselectBubbleGun?.Invoke();
        }
    }
}
