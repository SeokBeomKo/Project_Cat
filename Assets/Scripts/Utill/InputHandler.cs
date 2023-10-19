using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void PlayerInputHandle();
    public event PlayerInputHandle OnPlayerRunInput;
    public event PlayerInputHandle OnPlayerJumpInput;
    public event PlayerInputHandle OnPlayerDiveRollInput;

    private void Update() 
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            OnPlayerRunInput?.Invoke();
        }
        if (Input.GetButtonDown("Jump"))
        {
            OnPlayerJumpInput?.Invoke();
        }
        if (Input.GetButtonDown("DiveRoll"))
        {
            OnPlayerDiveRollInput?.Invoke();
        }
    }
}
