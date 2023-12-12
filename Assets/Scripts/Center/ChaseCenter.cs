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

    [Header("Input Handler")]
    [SerializeField] public InputHandler inputHandler;

    [SerializeField] public ChaseCat cat;

    [SerializeField] public RobotStart robotStart;

    [SerializeField] public RobotCleanerMovement robot;

    [SerializeField] public RobotAttack robotAttack;

    [SerializeField] public ChaseCameraController cameraController;
    public HairBallUse hairBallUse;

    private void Start()
    {
        mazeEnter.OnMazeEnter += OnMaze;

        cat.OnCutSceneStart += onCat;
        cat.OnCutSceneEnd += onPlay;

        robotStart.onRobot += onRobotStart;
        robotStart.onPlay += onPlay;

        robot.onMove += onMove;

        robotAttack.onRobotAttack += onRobotAttack;
        robotAttack.onShoot += onShoot;
        robotAttack.onPlay += onPlay;

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

        SoundManager.Instance.StopBGM();
        SoundManager.Instance.PlayBGM("Maze");
    }

    public void onCat()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetCatCamera();
    }

    public void onRobotStart()
    {
        Debug.Log("robot start");
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetRobotStartCamera();
    }

    public void onRobotAttack()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetRobotAttackCamera();
    }


    public void onPlay()
    {
        controllerUI.ShowUI();
        inputHandler.gameObject.SetActive(true);
        cameraController.SetPlayCamera();

    }

    public void onShoot()
    {
        robotAttack.targetPos = PlayerPosition();
    }

    public void onMove()
    {
        robot.PlayerPos = PlayerPosition();
    }

    public Vector3 PlayerPosition()
    {
        return Player.transform.position;
    }

 
}
