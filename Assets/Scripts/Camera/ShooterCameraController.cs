using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShooterCameraController : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera aimCamera;
    [SerializeField] public CinemachineVirtualCamera playCamera;
    [SerializeField] public CinemachineVirtualCamera topCamera;

    public void SetAimCamera()
    {
        aimCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
        topCamera.gameObject.SetActive(false);
    }
    public void SetPlayCamera()
    {
        aimCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(true);
        topCamera.gameObject.SetActive(false);
    }
    public void SetTopCamera()
    {
        aimCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(false);
        topCamera.gameObject.SetActive(true);
    }
}
