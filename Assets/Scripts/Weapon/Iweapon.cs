using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void EnterShoot();
    void ExcuteShoot();
    void ExitShoot();

    void Shoot();

    void SetTarget(Vector3 direction);
}
