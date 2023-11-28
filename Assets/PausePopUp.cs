using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePopUp : MonoBehaviour
{
    public GameObject settingPopUp;
    public GameObject pausePopUp;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePopUp.activeSelf)
            {
                pausePopUp.SetActive(false);
                settingPopUp.SetActive(false);
            }
            else
            {
                pausePopUp.SetActive(true);
            }
        }
    }

    public void OnClickResume()
    {
        Debug.Log("게임 계속");
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
}
