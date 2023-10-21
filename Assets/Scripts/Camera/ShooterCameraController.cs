using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShooterCameraController : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera aimCamera;
    public void SwitchCamera()
    {
        aimCamera.gameObject.SetActive(!aimCamera.gameObject.activeSelf);
    }
}
