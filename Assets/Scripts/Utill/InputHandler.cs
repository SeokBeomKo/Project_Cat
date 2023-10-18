using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void PlayerInputHandle();
    public event PlayerInputHandle OnPlayerRunInput;
    public event PlayerInputHandle OnPlayerJumpInput;

    private void Update() 
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            OnPlayerRunInput?.Invoke();
            return;
        }
        if (Input.GetButtonDown("Jump"))
        {
            OnPlayerJumpInput?.Invoke();
            return;
        }
    }
}
