using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCenter : MonoBehaviour
{
    public GameObject Player;

    [Header("카메라 회전")]
    [SerializeField] public CameraRotate camRotate;

    [Header("기본 입력 센터")]
    [SerializeField] public InputCenter inputCenter;

    [Header("미로 입력 센터")]
    [SerializeField] public ChaseInputCenter chaseInputCenter;

    [Header("미로 입장 판단")]
    [SerializeField] public MazeEnter mazeEnter;

    [Header("UI")]
    [SerializeField] public UIController controllerUI;

    [Header("퀘스트 UI")]
    public QuestPopUp questUI;

    [Header("퀘스트 위치")]
    public QuestSubtitle firstQuest; 

    [Header("자막")]
    public Subtitle subtitle;
    public QuestSubtitle questSubtitle;

    [Header("Input Handler")]
    [SerializeField] public InputHandler inputHandler;

    [Header("미로 입장 시 숨길 UI 그룹")]
    public CanvasGroup canvas;

    [Header("미로 입장 시 나타낼 UI 그룹")]
    public CanvasGroup mazeCanvas;

    [Header("추격씬 카메라 컨트롤러")]
    [SerializeField] public ChaseCameraController cameraController;

    [Header("아이템 휠")]
    public GameObject itemWheel;

    [Header("오브젝트")]
    [SerializeField] public ChaseCat cat;

    [SerializeField] public RobotStart robotStart;

    [SerializeField] public RobotCleanerMovement robot;

    [SerializeField] public RobotAttack robotAttack;

    [SerializeField] public FlyingObject[] flyObject;
    [SerializeField] public GameObject Object;

    public HairBallUse hairBallUse;
    // : <<


    private void Start()
    {
        PlayerPrefs.SetInt("Restart", 2);
        PlayerPrefs.SetInt("Camera", 10);
        questUI.DeactivatePopUp();
        flyObject = Object.GetComponentsInChildren<FlyingObject>();
        itemWheel.SetActive(true);
        camRotate.gameObject.SetActive(true);    
        
        
        firstQuest.OnQuest += MoveForward;

        robotStart.onRobot += onRobotStart; // 플레이어가 특정 지점에 가면 로봇 컷신
        robotStart.onPlay += onPlay;

        cat.OnCutSceneStart += onCat; // 고양이가 미로 옆에 가면 나오는 컷신
        cat.OnCutSceneEnd += onMazeCreate; // 미로 확대 컷신

        mazeEnter.OnMazeEnter += OnMaze; 

        robot.onMove += onMove; // 플레이어 위치 넘겨줌

        robotAttack.onRobotAttack += onRobotAttack; // 로봇이 공격하는 컷신
        robotAttack.onShoot += onShoot; // 플레이어 위치 전달
        robotAttack.onPlay += onMazePlay; // 미로 화면으로 전환

        inputCenter.gameObject.SetActive(true);
        chaseInputCenter.gameObject.SetActive(false);

    
        if (flyObject != null)
        { 
            for (int i = 0; i < flyObject.Length; i++)
            {
                int index = i;
                flyObject[index].onFly += () => onFly(index);
            }
        }

    }

    public void MoveForward()
    {
        questUI.ActivatePopUP("로키 따라가기", "로키를 따라 앞으로 이동하십시오.");
    }

    public void onRobotStart()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetRobotStartCamera();
        questUI.DeactivatePopUp();
        itemWheel.SetActive(false);
        subtitle.ShowSubtitle("카날리아 : 로봇 청소기에 빨려 들어가지 않도록 조심해야겠어!", delayTime : 0.2f);
    }
    public void onPlay()
    {
        //controllerUI.canvasUI = mazeCanvas;
        controllerUI.ShowUI();
        itemWheel.SetActive(true);
        inputHandler.gameObject.SetActive(true);
        cameraController.SetPlayCamera();
    }

    public void onMazePlay()
    {
        //controllerUI.canvasUI = mazeCanvas;
        controllerUI.ShowUI();
        itemWheel.SetActive(true);
        inputHandler.gameObject.SetActive(true);
        cameraController.SetTopCamera();
    }

    public void onCat()
    {
        itemWheel.SetActive(false);
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetCatCamera();
        subtitle.ShowSubtitle("카날리아 : 로키야 조심해!");
    }

    public void onMazeCreate()
    {
        cameraController.SetMazeCamera();
        StartCoroutine("MazeCameraMove");
        cat.transform.parent.gameObject.SetActive(false);
    }

    protected IEnumerator MazeCameraMove()
    {
        StartCoroutine(cameraController.MoveMazeCamera());
        yield return new WaitForSeconds(3);

        onPlay();
    }

    public void OnMaze()
    {
        SoundManager.Instance.StopBGM();

        inputCenter.gameObject.SetActive(false);
        chaseInputCenter.gameObject.SetActive(true);

        cameraController.SetTopCamera();
        SoundManager.Instance.PlayBGM("Maze");

        RemovePlayUI();
        controllerUI.canvasUI = mazeCanvas;
        controllerUI.ShowUI();
        itemWheel.SetActive(false);

        camRotate.gameObject.SetActive(false);
        subtitle.ShowSubtitle("카날리아 : 로봇 청소기가 고쳐지기 전에 미로를 벗어나야해!");
        questUI.ActivatePopUP("미로 돌파하기", "미로를 돌파하십시오.");
    }
    public void onMove()
    {
        robot.SetEndPos(PlayerPosition());
    }

    public void onRobotAttack()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetRobotAttackCamera();
        subtitle.ShowSubtitle("카날리아 : 로봇 청소기가 멈췄어...  근데 블록을 발사하잖아?!");
    }

    public void onShoot()
    {
        robotAttack.SetEndPos(PlayerPosition());
    }

    public void onFly(int index)
    {
        flyObject[index].SetEndPos(PlayerPosition());
    }

    public Vector3 PlayerPosition()
    {
        return Player.transform.position;
    }

    public void RemovePlayUI()
    {
        canvas.alpha = 0;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }
}
