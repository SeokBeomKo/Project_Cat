using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCenter : MonoBehaviour
{                                                                   
    [Header("플레이어 자막")]
    [SerializeField] public Subtitle subtitle;

    [Header("퀘스트 자막")]
    [SerializeField] public QuestSubtitle questSubtitle;

    [Header("UI")]
    [SerializeField] public UIController controllerUI;

    [Header("CCTV 퀘스트")]
    [SerializeField] public CCTV cctv;

    [Header("문 오브젝트")]
    [SerializeField] public Barrier barrier;

    [Header("벽 오브젝트")]
    [SerializeField] public Door door;

    [Header("스위치 오브젝트")]
    [SerializeField] public SwitchesOperation switches;

    [Header("카메라 시점")]
    [SerializeField] public ResearchCameraController cameraController;
    
    [Header("기본 입력 센터")]
    [SerializeField] public InputHandler inputHandler;

    [Header("도착 지점")]
    public GameObject endPoint;

    private void Start()
    {
        StartCoroutine(StartResearchScene());
        endPoint.SetActive(false);

        switches.InitSwitch(false);
        cctv.OnCCTV += ShutBarrier; // CCTV 켜기
        barrier.OnBarrier += ShutDoor; // 장벽 내려오기
        door.OnCloseDoor += OnSwitch; // 문 닫김
        switches.OnSwitch += UnlockDoor; // 스위치 퀘스트
        door.OnOpenDoor += ClearStage; // 문 열림
    }

    public IEnumerator StartResearchScene()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetIMacCamera();
        subtitle.ShowSubtitle("카날리아 : 저기 컴퓨터가 있어!  얼른 모든 문을 잠가야해!", delayTime: 1f);

        yield return new WaitForSeconds(6.5f);

        controllerUI.ShowUI();
        cameraController.SetPlayCamera();
        inputHandler.gameObject.SetActive(true);
    }

    public void ShutBarrier()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        StartCoroutine(barrier.MoveBarrierCoroutine());
        subtitle.ShowSubtitle("카날리아 : 좋아! 모든 문을 잠궜어!", speed: 0.09f, delayTime: 2.5f) ;
    }

    public void ShutDoor()
    {
        cameraController.SetDoorCamera();
        StartCoroutine(door.CloseDoor());
        subtitle.ShowSubtitle("카날리아 : 이런! 우리가 있는 방까지 잠겼잖아!");
    }

    public void OnSwitch()
    {
        inputHandler.gameObject.SetActive(true);
        cameraController.SetPlayCamera();
        subtitle.ShowSubtitle("카날리아 : 저기 있는 스위치를 이용하면 될 것 같은데?");
        switches.InitSwitch();
        controllerUI.ShowUI();
    }

    public void UnlockDoor()
    {
        controllerUI.RemoveUI();
        inputHandler.gameObject.SetActive(false);
        cameraController.SetDoorCamera();
        StartCoroutine(door.OpenDoor());
        subtitle.ShowSubtitle("카날리아 : 해냈다! 문이 열렸어!", delayTime : 1f);
    }

    public void ClearStage()
    {
        controllerUI.ShowUI();
        inputHandler.gameObject.SetActive(true);
        cameraController.SetPlayCamera();
        subtitle.ShowSubtitle("카날리아 : CCTV에 나온 로키의 위치로 가보자!", delayTime: 1f);
        endPoint.SetActive(true);
    }
}
