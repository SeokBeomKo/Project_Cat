using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    public delegate void UIInputHandle();
    public event UIInputHandle OnCleanlinessPopUpTrue;
    public event UIInputHandle OnCleanlinessPopUpFalse;
    public event UIInputHandle OnPausePopUp;
    //public event UIInputHandle OnItemWheel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnCleanlinessPopUpTrue?.Invoke();
        }
        if(Input.GetKeyUp(KeyCode.Tab))
        {
            OnCleanlinessPopUpFalse?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPausePopUp?.Invoke();
        }
        /*if (Input.GetMouseButtonDown(2))
        {
            OnItemWheel?.Invoke();
        }*/
    }
}
