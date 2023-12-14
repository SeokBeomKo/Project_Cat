using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShooterCameraController : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera aimCamera;
    [SerializeField] public CinemachineVirtualCamera playCamera;

    public void SetAimCamera()
    {
        aimCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);
    }
    public void SetPlayCamera()
    {
        aimCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(true);
    }
}
