using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTemp : MonoBehaviour
{
    void Awake()
    {
        DataManager.Instance.SetPlayerHP(100);
        
        DataManager.Instance.SetWeaponCurBullet(100, 100, 100);
        DataManager.Instance.SetWeaponMaxBullet(100, 100, 100);
    }
}
