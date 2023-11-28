using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePopUp : MonoBehaviour
{
    public delegate void PausePopupHandle();
    public event PausePopupHandle OnPausePopupTrue;
    public event PausePopupHandle OnPausePopupFalse;

    public GameObject settingPopUp;
    public GameObject pausePopUp;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePopUp.activeSelf)
            {
                pausePopUp.SetActive(false);
                Time.timeScale = 1f;
                OnPausePopupFalse?.Invoke();
            }
            else if(!pausePopUp.activeSelf && !settingPopUp.activeSelf)
            {
                pausePopUp.SetActive(true);
                Time.timeScale = 0f;
                OnPausePopupTrue?.Invoke();
            }
            else if(settingPopUp.activeSelf)
            {
                settingPopUp.SetActive(false);
                Time.timeScale = 1f;
                OnPausePopupFalse?.Invoke();
            }
        }
    }

    public void OnClickResume()
    {
        pausePopUp.SetActive(false);
        Time.timeScale = 1f;
        OnPausePopupFalse?.Invoke();
    }

    public void OnClickSetting()
    {
        pausePopUp.SetActive(false);
        settingPopUp.SetActive(true);
    }

    public void OnClickExit()
    {
        SceneManager.LoadScene("HanKyeol_Lobby");
    }

    public void ClosePopUp()
    {
        settingPopUp.SetActive(false);
        Time.timeScale = 1f;
        OnPausePopupFalse?.Invoke();
    }
}
