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

    public CanvasGroup canvas;

    public GameObject clean;
    public GameObject itemWheel;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.PlaySFX("Hover");

            if (pausePopUp.activeSelf)
            {
                pausePopUp.SetActive(false);
                Time.timeScale = 1f;
                OnPausePopupFalse?.Invoke();
                ShowUI();
            }
            else if(!pausePopUp.activeSelf && !settingPopUp.activeSelf)
            {
                pausePopUp.SetActive(true);
                Time.timeScale = 0f;
                OnPausePopupTrue?.Invoke();
                
                RemoveUI();
                
                itemWheel.SetActive(false);
                if (clean.gameObject != null)
                    clean.SetActive(false);
            }
            else if(settingPopUp.activeSelf)
            {
                settingPopUp.SetActive(false);
                Time.timeScale = 1f;
                OnPausePopupFalse?.Invoke();
                
                ShowUI();
            }
        }
    }

    public void OnClickResume()
    {
        SoundManager.Instance.PlaySFX("Click");
        pausePopUp.SetActive(false);
        ShowUI();
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
        SceneManager.LoadScene("HanKyeol_Lobby");
    }

    public void ClosePopUp()
    {
        SoundManager.Instance.PlaySFX("Click");
        settingPopUp.SetActive(false);
        Time.timeScale = 1f;
        OnPausePopupFalse?.Invoke();
        
        ShowUI();
    }

    public void RemoveUI()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    public void ShowUI()
    {
        canvas.alpha = 1;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;
    }
}
