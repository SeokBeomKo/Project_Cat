using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON_LIST
{
    SOAP_RIFLE,
    SPLASH_BUSTER,
    BUBBLE_GUN,
}

public class DataManager : Singleton<InventoryManager>
{
    private int playerCurHP;
    private int soapRifleCurBullet;
    private int splashBusterCurBullet;
    private int bubbleGunCurBullet;

    public int GetPlayerHP()
    {
        return playerCurHP;
    }

    public int GetWeaponBullet(WEAPON_LIST wepon)
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
    public void SetPlayerHP(int hp)
    {
        playerCurHP = hp;
    }

    public void SetWeaponBullet(int soapRifle, int splashBuster, int bubbleGun)
    {
        soapRifleCurBullet = soapRifle;
        splashBusterCurBullet = splashBuster;
        bubbleGunCurBullet = bubbleGun;
    }
}
