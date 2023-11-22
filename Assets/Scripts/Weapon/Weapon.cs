using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public abstract void SetTarget(Vector3 direction);
    public abstract void Shoot();
    public abstract void EnterShot();
    public abstract void ExcuteShot();
    public abstract void ExitShot();

}
