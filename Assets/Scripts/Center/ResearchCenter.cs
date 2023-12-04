using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCenter : MonoBehaviour
{                                                                   
    [Header("플레이어 자막")]
    [SerializeField] public Subtitle subtitle;

    [Header("퀘스트 자막")]
    [SerializeField] public QuestSubtitle questSubtitle;

    [Header("CCTV 퀘스트")]
    [SerializeField] public CCTV cctv;

    [Header("문")]
    [SerializeField] public Barrier barrier;

    [Header("벽")]
    [SerializeField] public Door door;

    [Header("카메라 시점")]
    [SerializeField] public ResearchCameraController cameraController;
    
    [Header("기본 입력 센터")]
    [SerializeField] public InputCenter inputCenter;


    private void Start()
    {
        cctv.OnCCTV += ShutBarrier; // CCTV 켜기
        barrier.OnBarrier += ShutDoor; // 장벽 내려오기
        door.OnCloseDoor += OnSwitch; // 문 닫김
    }

    public void ShutBarrier()
    {
        inputCenter.gameObject.SetActive(false);
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
        inputCenter.gameObject.SetActive(true);
        cameraController.SetPlayCamera();
        questSubtitle.ShowQuestSubtitle("버튼을 모두 올려 문을 개방하자", 0.09f);
    }
}
