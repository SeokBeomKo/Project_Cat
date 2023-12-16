using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitializater : MonoBehaviour
{
    void Start()
    {
        DataManager.Instance.SetPlayerHP(PlayerPrefs.GetInt("PlayerStatsPLAYER_MAX_HEALTH"));

        DataManager.Instance.SetWeaponBullet(PlayerPrefs.GetInt("SoapRifleWEAPON_MAX_BULLET"), PlayerPrefs.GetInt("SplashBusterWEAPON_MAX_BULLET"), PlayerPrefs.GetInt("BubbleGunWEAPON_MAX_BULLET"));
    }
}
