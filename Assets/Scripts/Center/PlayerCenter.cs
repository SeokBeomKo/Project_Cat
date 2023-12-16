using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCenter : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;
    
    [SerializeField]
    PlayerHitScan playerHitScan;

    [SerializeField]
    PlayerStats playerStats;

    [SerializeField]
    WeaponStrategy weaponStrategy;

    void Awake()
    {
        playerStats.SetCurHp(DataManager.Instance.GetPlayerHP());

        weaponStrategy.SetCurrentBullet(DataManager.Instance.GetWeaponBullet(WEAPON_LIST.SOAP_RIFLE), 
                                        DataManager.Instance.GetWeaponBullet(WEAPON_LIST.SPLASH_BUSTER), 
                                        DataManager.Instance.GetWeaponBullet(WEAPON_LIST.BUBBLE_GUN));
    }

    private void Start() 
    {
        weaponStrategy.weaponList[0].OnWeaponBullet += SaveBullet;
        playerHitScan.OnPlayerHitScan += HitPlayer;

        playerHitScan.OnPlayerSpeedUp += SpeedUp;
        playerHitScan.OnPlayerDamageUp += DamageUp;
    }

    public void SaveBullet()
    {
        //DataManager.Instance.SetWeaponBullet();
    }

    public void DamageUp()
    {
        weaponStrategy.DamageUp(5);
    }

    public void SpeedUp()
    {
        playerStats.AddMoveSpeed(5);
    }

    public void HitPlayer(int damage = 5)
    {
        playerStats.GetDamage(damage);

        if (0 >= playerStats.currentHealth)
        {
            playerController.stateMachine.ChangeStateAny(PlayerMovementStateEnums.DEAD);
        }
        else
        {
            playerController.stateMachine.ChangeStateAny(PlayerMovementStateEnums.STIFFEN);
        }
    }
}
