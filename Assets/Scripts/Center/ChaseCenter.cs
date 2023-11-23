using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCenter : MonoBehaviour
{
    [Header("카메라 회전")]
    [SerializeField] public CameraRotate camRotate;

    [Header("카메라 시점")]
    [SerializeField] public ShooterCameraController camController;

    [Header("기본 입력 센터")]
    [SerializeField] public InputCenter inputCenter;

    [Header("미로 입력 센터")]
    [SerializeField] public ChaseInputCenter chaseInputCenter;

    [Header("미로 입장 판단")]
    [SerializeField] public MazeEnter mazeEnter;

    [Header("종료 UI")]
    [SerializeField] public List<GameObject> endUIList;

    private void Start() 
    {
        mazeEnter.OnMazeEnter += OnMaze;

        inputCenter.gameObject.SetActive(true);
        chaseInputCenter.gameObject.SetActive(false);

        camController.SetPlayCamera();
        camRotate.gameObject.SetActive(true);
    }

    public void OnMaze()
    {
        inputCenter.gameObject.SetActive(false);
        chaseInputCenter.gameObject.SetActive(true);

        camController.SetTopCamera();
        camRotate.gameObject.SetActive(false);
        DisableUI();
    }

    private void DisableUI()
    {
        foreach(GameObject obj in endUIList)
        {
            obj.SetActive(false);
        }
    }
}
