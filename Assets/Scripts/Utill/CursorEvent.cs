using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CursorOff();
    }

    public void CursorOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
