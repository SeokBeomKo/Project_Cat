using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyScene : MonoBehaviour
{
    [Header("이동할 씬 이름")]
    public string sceneName;

    [Header("설정창")]
    public GameObject settingPopUp;

    public CanvasGroup canvas;

    private void Start()
    {
        settingPopUp.SetActive(false);
    }

    // 버튼 이벤트
    public void OnClickStart()
    {
        SoundManager.Instance.PlaySFX("Click");
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickSetting()
    {
        SoundManager.Instance.PlaySFX("Click");

        settingPopUp.SetActive(true);
        Time.timeScale = 0f;

        canvas.blocksRaycasts = false;
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlaySFX("Click");

#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }

    public void ClosePopUp()
    {
        SoundManager.Instance.PlaySFX("Click");

        settingPopUp.SetActive(false);
        Time.timeScale = 1f;

        canvas.blocksRaycasts = true;
    }
}


