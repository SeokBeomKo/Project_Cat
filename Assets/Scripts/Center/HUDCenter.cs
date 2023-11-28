using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDCenter : MonoBehaviour
{
    [Header("일시정지")]
    [SerializeField] public PausePopUp pausePopUp;

    [Header("정지 오브젝트")]
    [SerializeField] public GameObject cameraRotate;
    [SerializeField] public GameObject inputHandle;

    [Header("플레이어 스탯")]
    [SerializeField] public PlayerStats playerStats;

    [Header("플레이어 HUD")]
    [SerializeField] public HPObserver hpObserver;
    [SerializeField] public RollObserver rollObserver;

    private void Start() 
    {
        playerStats.AddObserver<IObserver>(playerStats.hpObserverList,hpObserver);
        playerStats.AddObserver<IObserver>(playerStats.rollObserverList,rollObserver);

        pausePopUp.OnPausePopupTrue += PauseTrue;
        pausePopUp.OnPausePopupFalse += PauseFalse;
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
