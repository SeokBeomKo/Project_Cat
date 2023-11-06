using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyScene : MonoBehaviour
{
    public void OnClickStart()
    {
        Debug.Log("게임 시작");
        SceneManager.LoadScene("HanKyeol_Loading");

    }

    public void OnClickSetting()
    {
        Debug.Log("옵션");
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR // 전처리 지시문
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 에디터에서 작동 안 함
#endif
    }
}


