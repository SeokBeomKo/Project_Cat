using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchCenter : MonoBehaviour
{
    // 게ㅔ임 흐름 뭐시기 어쩌구 나 못해

    // 자막 관리 ? // 언제 자막 나와야하는지 체크
    // 퀘스트 클리어 관리 .
    //

    [Header("플레이어 자막")]
    [SerializeField] public Subtitle subtitle;

    [Header("퀘스트 자막")]
    [SerializeField] public QuestSubtitle questSubtitle;

    [Header("CCTV 퀘스트")]
    [SerializeField] public CCTVOperation cctvOperation;
    
    [Header("카메라 시점")]
    [SerializeField] public ResearchCameraController cameraController;
    
    [Header("기본 입력 센터")]
    [SerializeField] public InputCenter inputCenter;



    
    private void Start()
    {
        cctvOperation.OnCloseBarrierTrue += OnBarrierTrue;
        cctvOperation.OnCloseBarrierFalse += OnBarrierFalse;
        cctvOperation.OnCloseDoorTrue += OnDoorTrue;
        cctvOperation.OnCloseDoorFalse += OnDoorFalse;
    }

    public void OnBarrierTrue()
    {
        inputCenter.gameObject.SetActive(false);
        subtitle.ShowSubtitle("카날리아 : 좋아! 모든 문을 잠궜어!");
    }

    public void OnBarrierFalse()
    {
        inputCenter.gameObject.SetActive(true);
    }

    public void OnDoorTrue()
    {
        inputCenter.gameObject.SetActive(false);
        cameraController.SetDoorCamera();
        subtitle.ShowSubtitle("카날리아 : 이런! 우리가 있는 방까지 잠겼잖아!");
    }

    public void OnDoorFalse()
    {
        inputCenter.gameObject.SetActive(true);
        cameraController.SetPlayCamera();
        questSubtitle.ShowQuestSubtitle("버튼을 모두 올려 문을 개방하자", 0.1f);

    }
}
