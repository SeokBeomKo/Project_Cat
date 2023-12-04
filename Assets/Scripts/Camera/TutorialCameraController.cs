using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TutorialCameraController : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera playCamera;
    [SerializeField] public CinemachineVirtualCamera aimCamera;

    [SerializeField] public CinemachineVirtualCamera catCamera;

    public void SetPlayCamera()
    {
        playCamera.gameObject.SetActive(true);
        aimCamera.gameObject.SetActive(false);

        catCamera.gameObject.SetActive(false);
    }

    public void SetCatCamera()
    {
        playCamera.gameObject.SetActive(false);
        aimCamera.gameObject.SetActive(false);

        catCamera.gameObject.SetActive(true);
    }
}
