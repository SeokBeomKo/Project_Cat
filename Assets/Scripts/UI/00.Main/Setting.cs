using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Setting : MonoBehaviour
{
    [Header("일시정지 팝업")]
    public GameObject pausePopUp;
    [Header("설정창 팝업")]
    public GameObject settingPopUp;

    [Header("세척도 팝업")]
    public GameObject clean;
    [Header("아이템 휠")]
    public GameObject itemWheel;

    [Header("로비씬")]
    public string lobbyName; 

    [Header("플레이 카메라")]
    public CinemachineVirtualCamera playCamera;

    public UIController uiController;
    public CursorEvent cursor;

    public delegate void PausePopupHandle();
    public event PausePopupHandle OnPausePopupTrue;
    public event PausePopupHandle OnPausePopupFalse;

    public void UpdatePause()
    {
        if (pausePopUp.activeSelf) // 일시정지 팝업 활성화 
        {
            PlayerPrefs.SetInt("Pause", 0);

            cursor.CursorOff();
            pausePopUp.SetActive(false);
            Time.timeScale = 1f;
            OnPausePopupFalse?.Invoke();

            if (playCamera.gameObject.activeSelf)
            {
                uiController.ShowUI();
                itemWheel.SetActive(true);
            }
        }
        else if (!pausePopUp.activeSelf && !settingPopUp.activeSelf) // 둘 다 비활성화
        {
            PlayerPrefs.SetInt("Pause", 1);

            cursor.CursorOn();
            pausePopUp.SetActive(true);
            Time.timeScale = 0f;
            OnPausePopupTrue?.Invoke();

            uiController.RemoveUI();
            itemWheel.SetActive(false);

            if (clean.gameObject != null)
                clean.SetActive(false);
        }
        else if (settingPopUp.activeSelf) // 설정창 활성화
        {
            PlayerPrefs.SetInt("Pause", 0);

            cursor.CursorOff();
            settingPopUp.SetActive(false);
            Time.timeScale = 1f;
            OnPausePopupFalse?.Invoke();

            if (playCamera.gameObject.activeSelf)
            {
                itemWheel.SetActive(true);
                uiController.ShowUI();
            }
        }
    }

    public void OnClickResume()
    {
        SoundManager.Instance.PlaySFX("Click");
        cursor.CursorOff();
        pausePopUp.SetActive(false);
        if (playCamera.gameObject.activeSelf)
        {
            itemWheel.SetActive(true);
            uiController.ShowUI();
        }
        Time.timeScale = 1f;
        OnPausePopupFalse?.Invoke();
    }

    public void OnClickSetting()
    {
        SoundManager.Instance.PlaySFX("Click");
        pausePopUp.SetActive(false);
        settingPopUp.SetActive(true);
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlaySFX("Click");
        Time.timeScale = 1f;
        SceneManager.LoadScene(lobbyName);
    }

    public void ClosePopUp()
    {
        SoundManager.Instance.PlaySFX("Click");
        cursor.CursorOff();
        settingPopUp.SetActive(false);
        Time.timeScale = 1f;
        OnPausePopupFalse?.Invoke();

        if (playCamera.gameObject.activeSelf)
        {
            itemWheel.SetActive(true);
            uiController.ShowUI();
        }
    }
    
}
