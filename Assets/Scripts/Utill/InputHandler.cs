using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public delegate void PlayerInputHandle();
    public event PlayerInputHandle OnPlayerRunInput;
    public event PlayerInputHandle OnPlayerJumpInput;
    public event PlayerInputHandle OnPlayerDiveRollInput;
    public event PlayerInputHandle OnPlayerAimSwitchInput;

    public delegate void WeaponInputHandle(int number);
    public event WeaponInputHandle OnWeaponSwapInput;

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
        if (Input.GetButtonDown("Fire2"))
        {
            OnPlayerAimSwitchInput?.Invoke();
        }

        if (Input.GetButtonDown("Swap0"))
        {
            OnWeaponSwapInput?.Invoke(0);
        }
        if (Input.GetButtonDown("Swap1"))
        {
            OnWeaponSwapInput?.Invoke(1);
        }
        if (Input.GetButtonDown("Swap2"))
        {
            OnWeaponSwapInput?.Invoke(2);
        }
    }
}
