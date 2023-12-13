using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    public delegate void UIInputHandle();
    public event UIInputHandle OnCleanlinessPopUpTrue;
    public event UIInputHandle OnCleanlinessPopUpFalse;
    public event UIInputHandle OnPausePopUp;

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
    }
}
