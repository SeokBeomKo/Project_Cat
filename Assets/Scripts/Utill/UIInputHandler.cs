using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInputHandler : MonoBehaviour
{
    public delegate void UIInputHandle();
    public event UIInputHandle OnCleanlinessPopUp;
    public event UIInputHandle OnPausePopUp;
    //public event UIInputHandle OnItemWheel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnCleanlinessPopUp?.Invoke();
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
