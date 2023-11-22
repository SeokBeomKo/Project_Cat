using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCenter : MonoBehaviour
{
    [SerializeField] public CameraRotate camRotate;
    [SerializeField] public ShooterCameraController camController;
    [SerializeField] public InputCenter inputCenter;
    [SerializeField] public ChaseInputCenter chaseInputCenter;

    public void OnMaze()
    {
        inputCenter.gameObject.SetActive(false);
        chaseInputCenter.gameObject.SetActive(true);

        camController.SetTopCamera();
        camRotate.gameObject.SetActive(false);
    }
}
