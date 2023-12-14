using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChaseCameraController : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField] public CinemachineVirtualCamera catCamera;
    [SerializeField] public CinemachineVirtualCamera mazeCamera;
    [SerializeField] public CinemachineVirtualCamera robotStartCamera;
    [SerializeField] public CinemachineVirtualCamera robotAttackCamera;
    [SerializeField] public CinemachineVirtualCamera playCamera;
    [SerializeField] public CinemachineVirtualCamera topCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void SetTopCamera()
    {
        PlayerPrefs.SetInt("Camera", 30);
        topCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
        robotAttackCamera.gameObject.SetActive(false);
    }

    public void SetCatCamera()
    {
        PlayerPrefs.SetInt("Camera", 20);
        catCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
    }

    public void SetMazeCamera()
    {
        PlayerPrefs.SetInt("Camera", 20);
        catCamera.gameObject.SetActive(false);
        mazeCamera.gameObject.SetActive(true);
    }

    public void SetRobotStartCamera()
    {
        PlayerPrefs.SetInt("Camera", 20);
        int cameraValue = PlayerPrefs.GetInt("Camera");
        Debug.Log("카메라 컨트롤러 : " + cameraValue);

        robotStartCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
    }

    public void SetRobotAttackCamera()
    {
        PlayerPrefs.SetInt("Camera", 20);
        robotAttackCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
    }

    public void SetPlayCamera()
    {
        Debug.Log("나 켜짐");
        PlayerPrefs.SetInt("Camera", 10);
        catCamera.gameObject.SetActive(false);
        mazeCamera.gameObject.SetActive(false);
        robotStartCamera.gameObject.SetActive(false);
        robotAttackCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(true);
    }

    public IEnumerator MoveMazeCamera()
    {
        float moveSpeed = 0.5f;
        float moveTime = 3.0f;
        float startTime = Time.time;

        while (Time.time - startTime < moveTime)
        {
            mazeCamera.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            yield return null;
        }


    }
}
