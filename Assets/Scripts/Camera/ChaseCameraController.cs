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


    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void SetCatCamera()
    {
        catCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
    }

    public void SetMazeCamera()
    {
        catCamera.gameObject.SetActive(false);
        mazeCamera.gameObject.SetActive(true);
    }

    public void SetRobotStartCamera()
    {
        robotStartCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
    }

    public void SetRobotAttackCamera()
    {
        robotAttackCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
    }

    public void SetPlayCamera()
    {
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
