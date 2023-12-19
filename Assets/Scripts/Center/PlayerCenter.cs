using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        LoadBullet();
    }

    private void Start() 
    {
        weaponStrategy.weaponList[0].OnWeaponBullet += UseBullet;
        weaponStrategy.weaponList[1].OnWeaponBullet += UseBullet;
        weaponStrategy.weaponList[2].OnWeaponBullet += UseBullet;
        playerHitScan.OnPlayerHitScan += HitPlayer;

        playerHitScan.OnPlayerSpeedUp += SpeedUp;
        playerHitScan.OnPlayerDamageUp += DamageUp;
    }

    public void LoadBullet()
    {
        weaponStrategy.SetCurrentBullet(DataManager.Instance.GetWeaponCurBullet(WEAPON_LIST.SOAP_RIFLE), 
                                        DataManager.Instance.GetWeaponCurBullet(WEAPON_LIST.SPLASH_BUSTER), 
                                        DataManager.Instance.GetWeaponCurBullet(WEAPON_LIST.BUBBLE_GUN));
    }

    public void UseBullet()
    {
        DataManager.Instance.SetWeaponCurBullet(weaponStrategy.weaponList[0].GetBullet(), weaponStrategy.weaponList[1].GetBullet(), weaponStrategy.weaponList[2].GetBullet());
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
            SceneManager.LoadScene("99.BadEnding");
        }
        else
        {
            playerController.stateMachine.ChangeStateAny(PlayerMovementStateEnums.STIFFEN);
        }
    }
}
