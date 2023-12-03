using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ResearchCameraController : MonoBehaviour
{
    Camera mainCamera;

    [SerializeField] public CinemachineVirtualCamera iMacCamera;
    [SerializeField] public CinemachineVirtualCamera doorCamera;
    [SerializeField] public CinemachineVirtualCamera playCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void SetIMacCamera()
    {
        iMacCamera.gameObject.SetActive(true);
        doorCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(false);

        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Item"));
    }

    public void SetDoorCamera()
    {
        iMacCamera.gameObject.SetActive(false);
        doorCamera.gameObject.SetActive(true);
        playCamera.gameObject.SetActive(false);

        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Item"));
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Enemy"));
        Camera.main.cullingMask = ~(1 << LayerMask.NameToLayer("Object"));
    }

    public void SetPlayCamera()
    {
        iMacCamera.gameObject.SetActive(false);
        doorCamera.gameObject.SetActive(false);
        playCamera.gameObject.SetActive(true);

        // CullingMask를 Everything으로 변경
        Camera.main.cullingMask = -1;
    }

}
