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
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickSetting()
    {
        settingPopUp.SetActive(true);
        Time.timeScale = 0f;

        canvas.blocksRaycasts = false;
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }

    public void ClosePopUp()
    {
        settingPopUp.SetActive(false);
        Time.timeScale = 1f;

        canvas.blocksRaycasts = true;
    }
}


