using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON_LIST
{
    SOAP_RIFLE,
    SPLASH_BUSTER,
    BUBBLE_GUN,
}

public class DataManager : Singleton<DataManager>
{
    private int playerCurHP;

    private int soapRifleCurBullet;
    private int splashBusterCurBullet;
    private int bubbleGunCurBullet;

    private int soapRifleMaxBullet;
    private int splashBusterMaxBullet;
    private int bubbleGunMaxBullet;

    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    public int GetPlayerHP()
    {
        return playerCurHP;
    }

    public void SetPlayerHP(int hp)
    {
        playerCurHP = hp;
    }

    public int GetWeaponMaxBullet(WEAPON_LIST wepon)
    {
        switch(wepon)
        {
            case WEAPON_LIST.SOAP_RIFLE:
                return soapRifleMaxBullet;
            case WEAPON_LIST.SPLASH_BUSTER:
                return splashBusterMaxBullet;
            case WEAPON_LIST.BUBBLE_GUN:
                return bubbleGunMaxBullet;
        }
        return 0;
    }

    public void SetWeaponMaxBullet(int soapRifle, int splashBuster, int bubbleGun)
    {
        soapRifleMaxBullet = soapRifle;
        splashBusterMaxBullet = splashBuster;
        bubbleGunMaxBullet = bubbleGun;
    }

    public int GetWeaponCurBullet(WEAPON_LIST wepon)
    {
        switch(wepon)
        {
            case WEAPON_LIST.SOAP_RIFLE:
                return soapRifleCurBullet;
            case WEAPON_LIST.SPLASH_BUSTER:
                return splashBusterCurBullet;
            case WEAPON_LIST.BUBBLE_GUN:
                return bubbleGunCurBullet;
        }
        return 0;
    }
    public void SetWeaponCurBullet(int soapRifle, int splashBuster, int bubbleGun)
    {
        soapRifleCurBullet = soapRifle;
        splashBusterCurBullet = splashBuster;
        bubbleGunCurBullet = bubbleGun;
    }

}
