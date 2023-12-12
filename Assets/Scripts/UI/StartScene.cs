using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public VideoPlayer video;
    public string sceneName;

    void Start()
    {
        video.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer video)
    {
        SceneManager.LoadScene(sceneName);
    }
}
