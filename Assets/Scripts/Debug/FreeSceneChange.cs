using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FreeSceneChange : MonoBehaviour
{
    public string lobbyName;
    public string tutorialName;
    public string researchName;
    public string chaseName;
    public string washingName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            SceneManager.LoadScene(lobbyName);
        else if (Input.GetKeyDown(KeyCode.K))
            SceneManager.LoadScene(tutorialName);
        else if (Input.GetKeyDown(KeyCode.J))
            SceneManager.LoadScene(researchName);
        else if (Input.GetKeyDown(KeyCode.H))
            SceneManager.LoadScene(chaseName);
        else if (Input.GetKeyDown(KeyCode.G))
            SceneManager.LoadScene(washingName);
    }
}
