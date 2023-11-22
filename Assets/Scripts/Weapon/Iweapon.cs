using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void EnterShot();
    void ExcuteShot();
    void ExitShot();
    
    void SetDirection(Vector3 direction);
}
