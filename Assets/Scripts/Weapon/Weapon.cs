using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    public abstract void SetDirection(Vector3 direction);
    public abstract void EnterShot();
    public abstract void ExcuteShot();
    public abstract void ExitShot();

}
