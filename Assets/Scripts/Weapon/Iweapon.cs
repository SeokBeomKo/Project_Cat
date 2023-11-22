using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void EnterShot();
    void ExcuteShot();
    void ExitShot();

    void Shoot();

    void SetTarget(Vector3 direction);
}
