using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public VideoPlayer video;
    public string sceneName;

    public bool videoEnd, loadEnd;
    void Start()
    {
        videoEnd = loadEnd = false;

        video.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer video)
    {
        videoEnd = true;
        ChangeScene();
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeScene()
    {
        if (videoEnd && loadEnd)
            SceneManager.LoadScene(sceneName);
    }
}
