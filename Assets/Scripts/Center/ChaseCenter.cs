using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCenter : MonoBehaviour
{
    public GameObject Player;
 
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

    [Header("UI")]
    [SerializeField] public UIController controllerUI;


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
        controllerUI.RemoveUI();
    }



    public Vector3 PlayerPosition()
    {
        Debug.Log("PlayerPos : " + Player.transform.position);
        return Player.transform.position;
    }
}
