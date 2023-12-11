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
        mazeCamera.gameObject.SetActive(false);
        robotStartCamera.gameObject.SetActive(false);
        robotAttackCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(false);


    }

    public void SetMazeCamera()
    {
        catCamera.gameObject.SetActive(false);
        mazeCamera.gameObject.SetActive(true);
        robotStartCamera.gameObject.SetActive(false);
        robotAttackCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(false);
    }

    public void SetRobotStartCamera()
    {
        catCamera.gameObject.SetActive(false);
        mazeCamera.gameObject.SetActive(false);
        robotStartCamera.gameObject.SetActive(true);
        robotAttackCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(false);
    }

    public void SetRobotAttackCamera()
    {
        catCamera.gameObject.SetActive(false);
        mazeCamera.gameObject.SetActive(false);
        robotStartCamera.gameObject.SetActive(false);
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

}
