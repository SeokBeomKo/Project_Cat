using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCenter : MonoBehaviour
{
    [Header("일시정지")]
    [SerializeField] public Setting setting;

    [Header("정지 오브젝트")]
    [SerializeField] public GameObject cameraRotate;
    [SerializeField] public GameObject inputHandle;

    [Header("무기 관리자")]
    [SerializeField] public WeaponStrategy weaponStrategy;

    [Header("플레이어 스탯")]
    [SerializeField] public PlayerStats playerStats;

    [Header("플레이어 무기")]
    [SerializeField] public Weapon soapRifle;
    [SerializeField] public Weapon splashBuster;
    [SerializeField] public Weapon bubbleGun;

    [Header("플레이어 HUD")]
    [SerializeField] public HPObserver hpObserver;
    [SerializeField] public RollObserver rollObserver;

    [SerializeField] public WeaponSelection weaponSelection;


    void Awake()
    {
        playerStats.AddObserver<IObserver>(playerStats.hpObserverList,hpObserver);
        playerStats.AddObserver<IObserver>(playerStats.rollObserverList,rollObserver);

        setting.OnPausePopupTrue += PauseTrue;
        setting.OnPausePopupFalse += PauseFalse;

        soapRifle.OnWeaponBullet += UpdateBullet;
        splashBuster.OnWeaponBullet += UpdateBullet;
        bubbleGun.OnWeaponBullet += UpdateBullet;

        weaponStrategy.OnSwapWeapon += UpdateWeapon;
    }

    private void Start() 
    {
        InitBullet();
        UpdateBullet();
    }

    public void InitBullet()
    {
        weaponSelection.SetMaxBullet(soapRifle.maxBullet, splashBuster.maxBullet, bubbleGun.maxBullet);
    }

    public void UpdateWeapon(int number)
    {
        weaponSelection.SetCurWeapon(number);
    }

    public void UpdateBullet()
    {
        weaponSelection.SelectSoftRifle(soapRifle.GetBullet(), soapRifle.maxBullet);
        weaponSelection.SelectSplashBuster(splashBuster.GetBullet(), splashBuster.maxBullet);
        weaponSelection.SelectBubbleGun(bubbleGun.GetBullet(), bubbleGun.maxBullet);
    }

    public void PauseTrue()
    {
        PlayerPrefs.SetInt("Pause", 1);
        cameraRotate.SetActive(false);
        inputHandle.SetActive(false);
    }

    public void PauseFalse()
    {
        PlayerPrefs.SetInt("Pause", 0);
        cameraRotate.SetActive(true);
        inputHandle.SetActive(true);
    }
}
