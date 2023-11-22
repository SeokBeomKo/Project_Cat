using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LobbyScene : MonoBehaviour
{
    [Header("이동할 씬 이름")]
    public string sceneName;

    public void OnClickStart()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickSetting()
    {
        Debug.Log("세팅");
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR 
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}


