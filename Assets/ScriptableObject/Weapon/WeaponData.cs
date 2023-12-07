using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public string colrowStartNumber;
    public string colrowEndNumber;
    public string sheetNumber;
    
    public int maxBullet;
    public int attackPower;
    public float shootDelay;
}
