using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInitializater : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetInt("SoapRifleWEAPON_CURRENT_BULLET",0);
        PlayerPrefs.SetInt("SplashBusterWEAPON_CURRENT_BULLET",0);
        PlayerPrefs.SetInt("BubbleGunWEAPON_CURRENT_BULLET",0);
    }

    void Start()
    {
        DataManager.Instance.SetPlayerHP(PlayerPrefs.GetInt("PlayerStatsPLAYER_MAX_HEALTH"));
        
        DataManager.Instance.SetWeaponCurBullet(PlayerPrefs.GetInt("SoapRifleWEAPON_CURRENT_BULLET"), PlayerPrefs.GetInt("SplashBusterWEAPON_CURRENT_BULLET"), PlayerPrefs.GetInt("BubbleGunWEAPON_CURRENT_BULLET"));
        DataManager.Instance.SetWeaponMaxBullet(PlayerPrefs.GetInt("SoapRifleWEAPON_MAX_BULLET"), PlayerPrefs.GetInt("SplashBusterWEAPON_MAX_BULLET"), PlayerPrefs.GetInt("BubbleGunWEAPON_MAX_BULLET"));
    }
}
